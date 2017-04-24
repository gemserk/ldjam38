using UnityEditor;
using UnityEngine;

namespace Gemserk.LD38.Game
{
    [ExecuteInEditMode]
    public class GameCameraFreeRails : GameCamera
    {
        public Transform cameraTransform;

        bool transitioning;

        Vector3 targetPosition;

        public float transitionSpeed = 10.0f;

        public AnimationCurve movementCurve;

        public Transform railStartPosition;
        public Transform railEndPosition;

        public float startingRailPosition = 0.1f;

        [Range(0.0f, 1.0f)]
        public float currentRailPosition;

        public Vector3 cameraOffset;

        public float oldRailPosition;

        void Start()
        {
            oldRailPosition = startingRailPosition;
            SetRailPosition (startingRailPosition);
        }

        public override bool IsTransitioning()
        {
            return transitioning;
        }

        public override void SetRailPosition(float t)
        {
            //targetPosition = GetRailsPosition(t);
            transitioning = true;
            currentRailPosition = t;
        }

        public Vector3 GetRailsPosition(float t)
        {
            return Vector3.Lerp (railStartPosition.transform.position, railEndPosition.transform.position, t);
        }

        public override void MoveRailPosition(float direction)
        {
            currentRailPosition = Mathf.Clamp (currentRailPosition + direction, 0, 1);
            SetRailPosition (currentRailPosition);
        }

        public void LateUpdate()
        {
            if (!transitioning)
                return;
//
            var newRailPosition = Mathf.Lerp(oldRailPosition, currentRailPosition,
                Time.deltaTime * transitionSpeed);

            var newCameraTarget = GetRailsPosition(newRailPosition);

            cameraTransform.position = newCameraTarget + cameraOffset;
            cameraTransform.LookAt(newCameraTarget);
            transitioning = Vector3.Distance (cameraTransform.position, targetPosition) > 1.0f;
            oldRailPosition = newRailPosition;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetPosition, 1);

        }
    }
}