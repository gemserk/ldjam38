using System.ComponentModel;
using UnityEditor;
using UnityEngine;

namespace Gemserk.LD38.Game
{
    [CustomEditor(typeof(GameCameraFreeRails)), CanEditMultipleObjects]
    public class GameCameraFreeRailsEditor : Editor
    {
//        public override void OnInspectorGUI()
//        {
//            base.OnInspectorGUI();
//            (target as GameCameraFreeRails).LateUpdate();
//        }

//        public void OnSceneGUI()
//        {
//            GameCameraFreeRails component = (GameCameraFreeRails)target;
//
//            EditorGUI.BeginChangeCheck();
//            var cameraHandlePos = component.cameraTransform.position;
//            Quaternion quaternion = Quaternion.LookRotation(component.cameraTransform.forward, component.cameraTransform.up);
//             Vector3 newCameraPosition = Handles.PositionHandle(cameraHandlePos, quaternion);
//
//            if (EditorGUI.EndChangeCheck())
//            {
//                var cameraTarget = component.GetRailsPosition(component.oldRailPosition);
//                Undo.RecordObject(target, "Change camera offset");
//                component.cameraOffset = newCameraPosition - cameraTarget;
//                component.LateUpdate();
//            }
//        }
    }
}