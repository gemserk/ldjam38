using UnityEngine;
using Assets.Scripts.Game.Weapons;
using System;

public class WeaponControl : MonoBehaviour
{
	Weapon weapon;

	public Transform attachPoint;

	public float aimSpeed = 10.0f;

	// TODO: charge curve....
	public float chargeSpeed = 10.0f;

	public float chargeCooldown = 1.0f;

	float lastChargingTime;

	float charge;

	bool isCharging;

	public float GetCharge()
	{
		return charge;
	}

	public Weapon GetWeapon()
	{
		return weapon;
	}

	public void Equip(Weapon newWeapon)
	{
		if (weapon != null) {
			GameObject.Destroy (weapon);
			weapon = null;
		}

		this.weapon = newWeapon;

		if (weapon != null) {
			weapon.transform.SetParent (attachPoint, false); 
		}

	}

	public void Load()
	{
		if (weapon != null) {
			// TODO: delegate to the weapon for animations, etc
			weapon.gameObject.SetActive (true);
		}
	}

	public void Unload()
	{
		if (weapon != null)
			weapon.gameObject.SetActive (false);
	}

	public void Aim(float direction)
	{
		weapon.transform.Rotate (direction * aimSpeed, 0, 0);
	}

	public bool IsLoaded()
	{
		if (weapon == null)
			return false;
		return weapon.gameObject.activeSelf;
	}

	public bool IsChargingAttack ()
	{
		return isCharging;
	}

	public void ChargeAttack (bool charging, float dt, Action<WeaponControl> callback)
	{
		if (weapon == null)
			return;

		if (!isCharging && charging) {

			if (Time.realtimeSinceStartup - lastChargingTime < chargeCooldown)
				return;

			charge = 0;
			isCharging = true;

			return;
		}

		if (isCharging) {

			charge += dt * chargeSpeed;

			if (charge > 1 || !charging) {
				weapon.Fire (charge);

				if (callback != null)
					callback (this);

				isCharging = false;

				lastChargingTime = Time.realtimeSinceStartup;
		
				charge = 0.0f;
			}
		}

	}

}
