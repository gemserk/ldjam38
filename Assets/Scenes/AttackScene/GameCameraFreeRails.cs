﻿using Gemserk.Utils;
 using UnityEngine;

namespace Gemserk.LD38.Game
{
    [ExecuteInEditMode]
    public class GameCameraFreeRails : GameCamera
    {
        public Transform cameraTransform;


        [ReadOnly] [SerializeField] private bool transitioning;

        Vector3 targetPosition;

        public float transitionSpeed = 10.0f;

        public AnimationCurve movementCurve;

        public Transform railStartPosition;
        public Transform railEndPosition;

        public float startingRailPosition = 0.1f;

        [Range(0.0f, 1.0f)] public float currentRailPosition;

        public Vector3 _cameraOffset;

        public float cameraDistance;
        public float yaw;
        public float pitch;


        public float minDistance;
        public float maxDistance;

        public float minDistanceToRail;
        public float maxDistanceToRail;


        public Transform dummyCenter;
        public Transform dummyPoint;

        public Vector3 cameraOffset
        {
            get
            {
                dummyPoint.transform.localPosition = new Vector3(cameraDistance, 0, 0);
                dummyCenter.localEulerAngles = new Vector3(0, yaw, pitch);

                var offset = dummyPoint.position - dummyCenter.position;
                dummyOffset = offset;
                return offset;
            }
        }

        public Vector3 dummyOffset;

        public float oldRailPosition;

        public float targetCameraDistance;

        void Start()
        {
            oldRailPosition = startingRailPosition;
            SetRailPosition(startingRailPosition);
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
            return Vector3.Lerp(railStartPosition.transform.position, railEndPosition.transform.position, t);
        }

        public override void CenterOn(Vector3 position)
        {
            var closestPoint = GetClosestPointToLine(railStartPosition.position, railEndPosition.position, position);
            var totalDistance = Vector3.Distance(railStartPosition.position, railEndPosition.position);
            var distanceFromStart = Vector3.Distance(railStartPosition.position, closestPoint);
            var t = Mathf.Clamp01(distanceFromStart / totalDistance);

            float distanceToLine = Vector3.Distance(closestPoint, position);

            var cameraDistanceInterpolated = Util.interpolate(minDistanceToRail, maxDistanceToRail, minDistance,
                maxDistance, distanceToLine);

            targetCameraDistance = cameraDistanceInterpolated;

            SetRailPosition(t);
        }

        static public Vector3 GetClosestPointToLine(Vector3 a, Vector3 b, Vector3 point)
        {
            Vector3 AP = point - a; //Vector from A to P
            Vector3 AB = b - a; //Vector from A to B

            float magnitudeAB = AB.sqrMagnitude; //Magnitude of AB vector (it's length squared)
            float ABAPproduct = Vector3.Dot(AP, AB); //The DOT product of a_to_p and a_to_b
            float distance = ABAPproduct / magnitudeAB; //The normalized "distance" from a to your closest point

            if (distance < 0) //Check if P projection is over vectorAB
            {
                return a;
            }
            else if (distance > 1)
            {
                return b;
            }
            else
            {
                return a + AB * distance;
            }
        }

        public override void MoveRailPosition(float direction)
        {
            currentRailPosition = Mathf.Clamp(currentRailPosition + direction, 0, 1);
            SetRailPosition(currentRailPosition);
        }

        public void LateUpdate()
        {
            var newRailPosition = oldRailPosition;
            if (!Application.isPlaying || transitioning)
            {
//
                newRailPosition = Mathf.Lerp(oldRailPosition, currentRailPosition,
                    Time.deltaTime * transitionSpeed);

                if (Mathf.Abs(newRailPosition - currentRailPosition) < 0.1f)
                {
                    newRailPosition = currentRailPosition;
                    transitioning = false;
                }
            }

            cameraDistance = Mathf.Lerp(cameraDistance, targetCameraDistance, Time.deltaTime * transitionSpeed);

            var newCameraTarget = GetRailsPosition(newRailPosition);

            cameraTransform.position = newCameraTarget + cameraOffset;
            cameraTransform.LookAt(newCameraTarget);

            oldRailPosition = newRailPosition;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(targetPosition, 1);
        }
    }
}