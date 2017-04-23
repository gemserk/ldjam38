using UnityEngine;
using Assets.Scripts.Game.Weapons;

public class Character : MonoBehaviour {

	Hud hud;

	public WeaponControl weaponControl;

	GameMode gameMode;

	public void SetHud(Hud hud)
	{
		this.hud = hud;
	}

	public void SetGameMode(GameMode gameMode)
	{
		this.gameMode = gameMode;	
	}

	public bool InAttackMode ()
	{
		return weaponControl.IsLoaded ();
	}

	public void Equip(Weapon newWeapon)
	{
		weaponControl.Equip (newWeapon);
	}

	public void EnterWalkMode()
	{
		weaponControl.Unload ();
	}

	public void EnterAttackMode()
	{
		weaponControl.Load ();
	}

	public void AimWeapon(float direction)
	{
		weaponControl.Aim (direction);
	}

	public bool IsChargingAttack ()
	{
		return weaponControl.IsChargingAttack ();
	}

	public void ChargeAttack (bool charging)
	{
		weaponControl.ChargeAttack (charging, Time.deltaTime, delegate(WeaponControl w) {
			if (gameMode != null)
				gameMode.OnCharacterFired(this);
		});

		if (hud != null) {
			if (hud.chargeIndicator != null)
				hud.chargeIndicator.UpdateCharge (weaponControl);
		}
	}

}
