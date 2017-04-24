using UnityEngine;

namespace Gemserk.Utils
{
   // [ExecuteInEditMode]
    public class CenterOnTester : MonoBehaviour
    {
        public Transform startRail;
        public Transform endRail;
        public Transform targetPoint;



        private void OnDrawGizmos()
        {
            if (startRail == null || endRail == null || targetPoint == null)
            {
                return;
            }

           // var centeredPosition = GetClosestPointToLine(startRail.position, endRail.position, targetPoint.position);

            var centeredPosition = CenterOn(targetPoint.position);

            Gizmos.DrawSphere(centeredPosition, 3);
        }

        public Vector3 GetRailsPosition(float t)
        {
            return Vector3.Lerp (startRail.transform.position, endRail.transform.position, t);
        }

        public Vector3  CenterOn(Vector3 position)
        {
            var closestPoint = GetClosestPointToLine(startRail.position, endRail.position, position);
            var totalDistance = Vector3.Distance(startRail.position, endRail.position);
            var distanceFromStart = Vector3.Distance(startRail.position, closestPoint);
            var t = Mathf.Clamp01(distanceFromStart / totalDistance);
            return GetRailsPosition(t);
        }

        static public Vector3 GetClosestPointToLine(Vector3 a, Vector3 b, Vector3 point)
        {
            Vector3 AP = point - a;       //Vector from A to P
            Vector3 AB = b - a;       //Vector from A to B

            float magnitudeAB = AB.sqrMagnitude;     //Magnitude of AB vector (it's length squared)
            float ABAPproduct = Vector3.Dot(AP, AB);    //The DOT product of a_to_p and a_to_b
            float distance = ABAPproduct / magnitudeAB; //The normalized "distance" from a to your closest point

            if (distance < 0)     //Check if P projection is over vectorAB
            {
                return a;

            }
            else if (distance > 1)             {
                return b;
            }
            else
            {
                return a + AB * distance;
            }
        }
    }
}