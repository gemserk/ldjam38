using UnityEngine;

namespace Gemserk.Utils
{
    public class RotatorComponent : MonoBehaviour
    {
        public Transform theTransform;
        public float speed;
        public Vector3 axis;

        public void Update()
        {
            theTransform.Rotate(axis, Time.deltaTime * speed);
        }
    }
}