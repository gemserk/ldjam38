using UnityEngine;
using Assets.Scripts.Game.Weapons;

namespace Assets.Scripts.Game.Weapons
{
	public interface ProjectileHitReceiver {

		void OnProjectileHit(Bomb bomb);

	}

}