using UnityEngine;

namespace Assets.Scripts.Game.Weapons
{
    public class ProjectileWeapon : Weapon
    {
        public GameObject projectile;

        public Transform fireTransform;
        public Transform centerTransform;

        public AnimationCurve forceResponse;
        public float forceMultiplier;

        public override void Fire(float charge)
        {
            var direction = (fireTransform.position - centerTransform.position).normalized;
            GameObject go = GameObject.Instantiate(projectile);
            go.transform.position = fireTransform.position;
            var rigidBody = go.GetComponent<Rigidbody>();

            var force = direction.normalized * forceMultiplier * forceResponse.Evaluate(charge);

            rigidBody.AddForce(force, ForceMode.Impulse);
        }
    }
}