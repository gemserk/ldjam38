using Assets.Scripts.Game.Weapons;

namespace Assets.Scripts.Game.Weapons
{
	public struct ProjectileHit 
	{
		public Bomb projectile;
	}

	public interface ProjectileHitReceiver {

		void OnProjectileHit(ProjectileHit hit);

	}

}