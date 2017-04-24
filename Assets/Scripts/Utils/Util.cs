using System.Collections.Generic;
using System.Linq;
using Gemserk.LD38.Game.World;
using UnityEngine;

namespace Gemserk.Utils
{
    public class Util
    {
        public static IEnumerable<GameObject> GetWorldCubesInColumn(Vector3 position)
        {
            var axis = Vector3.down;
            Ray ray = new Ray(position - axis * 1000, axis);

            var hits = Physics.RaycastAll(ray);

            return hits.OrderBy(hit => hit.distance)
                .Select(hit => hit.collider.gameObject.GetComponentInParent<WorldCube>())
                .Where(worldCube => worldCube != null)
                .Select(worldCube => worldCube.gameObject);

        }
    }
}