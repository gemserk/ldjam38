using UnityEditor;
using UnityEngine;

namespace Gemserk.LD38.Game
{
    [CustomEditor(typeof(GameCameraFreeRails)), CanEditMultipleObjects]
    public class GameCameraFreeRailsEditor : Editor
    {
        public void OnSceneGUI()
        {
            GameCameraFreeRails component = (GameCameraFreeRails)target;

            EditorGUI.BeginChangeCheck();
            var cameraHandlePos = component.cameraTransform.position;
            Vector3 newCameraPosition = Handles.PositionHandle(cameraHandlePos, Quaternion.identity);

            if (EditorGUI.EndChangeCheck())
            {
                var cameraTarget = component.GetRailsPosition(component.oldRailPosition);
                Undo.RecordObject(target, "Change camera offset");
                component.cameraOffset = newCameraPosition - cameraTarget;
                component.LateUpdate();
            }
        }
    }
}