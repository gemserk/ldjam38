using Assets.Scripts.Game.Weapons;
using UnityEngine;

namespace Assets.Scripts.Game.Weapons
{
	public class ProjectileHitReceiverDelegate : MonoBehaviour, ProjectileHitReceiver
	{
		public GameObject projectileHitReceiverDelegateObject;

		public void OnProjectileHit (Bomb bomb)
		{
			if (projectileHitReceiverDelegateObject == null)
				return;
			var projectileDelegate = projectileHitReceiverDelegateObject.GetComponent<ProjectileHitReceiver> ();
			if (projectileDelegate != null)
				projectileDelegate.OnProjectileHit (bomb);
		}
	}}