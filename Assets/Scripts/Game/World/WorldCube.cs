using UnityEngine;
using Assets.Scripts.Game.Weapons;

namespace Gemserk.LD38.Game.World
{
	public class WorldCube : MonoBehaviour, ProjectileHitReceiver
    {
        public Vector3 oldPosition;

        public int row;
        public int column;
        public int height;

        public void Awake()
        {
            var pos = this.transform.position;
            row = Mathf.RoundToInt(pos.x);
            column = Mathf.RoundToInt(pos.y);
            column = Mathf.RoundToInt(pos.z);
            oldPosition = pos;
        }

		#region ProjectileHitReceiver implementation

		public void OnProjectileHit (Bomb bomb)
		{
			GameObject.Destroy (this.gameObject);
		}

		#endregion
    }
}

