using UnityEngine;

namespace Assets.Scripts.Game.Weapons
{
	public abstract class Weapon : MonoBehaviour
    {
		public abstract void Fire (float charge);

		public abstract Transform GetFireTransform();
    }
}