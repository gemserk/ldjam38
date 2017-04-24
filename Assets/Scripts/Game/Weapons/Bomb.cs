using UnityEngine;

namespace Assets.Scripts.Game.Weapons
{
	public class Bomb : MonoBehaviour, ProjectileHitReceiver
    {
        public float explodeRadius;

		public GameObject explosionEffectPrefab;
        private Bounds? worldBounds = null;

        public void Start()
        {
            var worldBoundsGO = GameObject.Find("WorldBounds");
            if (worldBoundsGO != null)
            {
                var boxCollider = worldBoundsGO.GetComponent<BoxCollider>();
                var boxColliderCenter = boxCollider.transform.localToWorldMatrix.MultiplyPoint3x4(boxCollider.center);
                worldBounds = new Bounds(boxColliderCenter, boxCollider.size);
            }
        }

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

            AddExplodeFX();
		}

        public void AddExplodeFX()
        {
            if (explosionEffectPrefab != null) {
                var explosionEffect = GameObject.Instantiate (explosionEffectPrefab);
                explosionEffect.transform.position = this.transform.position;
            }
        }

        public void Update()
        {
            if (worldBounds.HasValue)
            {
                Bounds bounds = worldBounds.Value;
                if (!bounds.Contains(this.transform.position))
                {
                    AddExplodeFX();
                    GameObject.Destroy(this.gameObject);
                }
            }
        }

		#region ProjectileHitReceiver implementation

		public void OnProjectileHit (Bomb bomb)
		{
			GameObject.Destroy (this.gameObject);
		}

		#endregion
    }
}