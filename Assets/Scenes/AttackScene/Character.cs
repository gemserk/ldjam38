using UnityEngine;
using Assets.Scripts.Game.Weapons;

public class Character : MonoBehaviour {

	public Transform weaponAttachPoint;

	public void Equip(Weapon weapon)
	{
		weapon.transform.SetParent (weaponAttachPoint, false); 
	}

}
