using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Gemserk.Utils
{


    public static class TransformExtension
    {
        /// <summary>
        /// Gives an enumerator for the direct children of a transform, generates garbage
        /// </summary>
        /// <param name="includeCurrent">if true includes the current transform in the enumeration</param>
        public static IEnumerable<Transform> GetChildren(this Transform theTransform, bool includeCurrent)
        {
            if (includeCurrent)
            {
                yield return theTransform;
            }

            for (int i = 0; i < theTransform.childCount; i++)
            {
                var child = theTransform.GetChild((i));
                yield return child;
            }
        }

        /// <summary>
        /// Gives an enumerator for all the decendants of a transform, generates garbage
        /// </summary>
        /// <param name="includeCurrent">if true includes the current transform in the enumeration</param>
        public static IEnumerable<Transform> GetChildrenRecursive(this Transform theTransform, bool includeCurrent)
        {
            if (includeCurrent)
            {
                yield return theTransform;
            }

            for (int i = 0; i < theTransform.childCount; i++)
            {
                var child = theTransform.GetChild((i));
                foreach (var grandson in GetChildrenRecursive(child, true))
                {
                    yield return grandson;
                }
            }
        }

        public static void ResetLocal(this Transform t)
        {
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
        }




        #region Collections

        public static void MergeWith<K, V>(this Dictionary<K, V> orig, Dictionary<K, V> newData, bool replaceDuplicates)
        {
            if (replaceDuplicates)
            {
                foreach (var entry in newData)
                {
                    orig[entry.Key] = entry.Value;
                }
            }
            else
            {
                foreach (var entry in newData)
                {
                    orig.Add(entry.Key, entry.Value);
                }
            }
        }


        public static void RemoveAsBag<T>(this List<T> list, T item)
        {
            int indexOf = list.IndexOf(item);
            if (indexOf == -1)
                return;

            var lastIndex = list.Count - 1;

            list[indexOf] = list[lastIndex];
            list.RemoveAt(lastIndex);
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,TKey key,TValue defaultValue)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static TValue GetValueOrDefaultFunc<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,TKey key,Func<TValue> defaultValueProvider)
        {
            TValue value;
            return dictionary.TryGetValue(key, out value) ? value
                : defaultValueProvider();
        }

        public static TValue GetOrNew<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,TKey key) where TValue:new()
        {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
            {
                value = new TValue();
                dictionary.Add(key, value);
            }

            return value;
        }

        public static string ToStringArray<T>(this IEnumerable<T> enumerable, string separator, Func<T, string> func){
            return String.Join (separator, enumerable.Select (func).ToArray ());
        }

        public static string ToStringArray<T>(this IEnumerable<T> enumerable, string separator){
            return String.Join (separator, enumerable.Select ((arg) => arg.ToString()).ToArray ());
        }

        public static void ForEach<T>(this IEnumerable<T> ie, Action<T> action)
        {
            foreach (var i in ie)
            {
                action(i);
            }
        }

        #endregion

        #region MyRegion

        public static string ToStringDetailed(this Vector2 v) {
            return String.Format ("{{{0}, {1}}}", v.x, v.y);
        }

        public static Vector2 ScalarMultiplication(this Vector2 v, Vector2 other)
        {
            return new Vector2(v.x * other.x, v.y * other.y);
        }

        #endregion


    }
}