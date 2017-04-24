using UnityEngine;
using Assets.Scripts.Game.Weapons;

public class Character : MonoBehaviour, ProjectileHitReceiver {

	Hud hud;

	public WeaponControl weaponControl;

	GameMode gameMode;

	public CharacterModel characterModel;

	public float rotationSpeed = 15.0f;

	float walkRotation;
	float attackRotation;

    public bool IsDead = false;

	void Start()
	{
		walkRotation = transform.localEulerAngles.y;
		attackRotation = transform.localEulerAngles.y;
	}

	public void SetHud(Hud hud)
	{
		this.hud = hud;
	}

	public void SetGameMode(GameMode gameMode)
	{
		this.gameMode = gameMode;	
	}

	#region ProjectileHitReceiver implementation
	public void OnProjectileHit (Bomb bomb)
	{
		// if no life then die...

		if (characterModel != null)
			characterModel.DamageReceived ();

		gameMode.OnCharacterDeath (this);
	}
	#endregion

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
		transform.localEulerAngles = new Vector3(0, walkRotation, 0);
	}

	public void EnterAttackMode()
	{
		weaponControl.Load ();
		transform.localEulerAngles = new Vector3(0, attackRotation, 0);
	}

	public void AimWeapon(float direction)
	{
		weaponControl.Aim (direction);
	}

	public void Rotate (float direction)
	{
		transform.Rotate(new Vector3(0, direction * rotationSpeed, 0));
		attackRotation = transform.localEulerAngles.y;
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
