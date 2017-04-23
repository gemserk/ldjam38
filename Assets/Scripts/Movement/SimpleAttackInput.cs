using UnityEngine;

public class SimpleAttackInput : MonoBehaviour {

	public string verticalAxisName;

	public string weaponActionName;
	public string chargeActionName;

	public string cancelActionName;

	public Character character;

	public void Update()
	{
		if (character == null)
			return;

		if (character.InAttackMode ()) {

			if (!character.IsChargingAttack ()) {
				if (Input.GetButtonUp (cancelActionName)) {
					character.EnterWalkMode ();
					return;
				}
			}

			var vertical = Input.GetAxis (verticalAxisName);

			character.AimWeapon(vertical * Time.deltaTime);
			character.ChargeAttack (Input.GetButton (chargeActionName));

		} else {

			if (Input.GetButtonUp (weaponActionName)) {
				character.EnterAttackMode ();
			}
		
		}	
	}

}
