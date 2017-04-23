﻿using UnityEngine;

namespace Assets.Scripts.Game.Weapons
{
    public class Bomb : MonoBehaviour
    {
        public float explodeRadius;

        private void OnCollisionEnter(Collision other)
        {
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, explodeRadius);
            foreach (var collider in hitColliders)
            {
                if (collider.gameObject == this.gameObject) 
					continue;

				var projectileHitReceiver = collider.GetComponent<ProjectileHitReceiver> ();

				if (projectileHitReceiver != null) {
					projectileHitReceiver.OnProjectileHit (this);
				} else {
					GameObject.Destroy (collider.gameObject);
				}
			}
        }
    }
}