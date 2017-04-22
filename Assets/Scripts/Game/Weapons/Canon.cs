using System;
using UnityEngine;

namespace Assets.Scripts.Game.Weapons
{
    public class Canon : MonoBehaviour
    {
        public GameObject projectile;
        public float forceStrength;

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                GameObject go = GameObject.Instantiate(projectile);
                var rigidBody = go.GetComponent<Rigidbody>();
                var force = UnityEngine.Random.insideUnitSphere;
                //force.x = Mathf.Abs(force.x);
                force.y = Mathf.Abs(force.y);
               // force.z = Mathf.Abs(force.z);
                force = force * forceStrength;

                rigidBody.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}