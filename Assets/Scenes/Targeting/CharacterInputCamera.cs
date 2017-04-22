using UnityEngine;
using Assets.Scripts.Game.Weapons;

public class CharacterInputCamera : MonoBehaviour {

	public enum ControllerMode
	{
		MovementMode,
		AttackingMode
	}

	public string horizontalAxisName;
	public string verticalAxisName;
	public string shootButtonName;

	public string switchModeButtonName;

	float vertical;
	float horizontal;

	public Transform cameraTransform;

	public Rigidbody body;

	public float speed = 10.0f;
	public float rotationSpeed = 10.0f;

	bool shootButton;
	bool switchModeButton;

	public Weapon weapon;

	public ControllerMode controllerMode = ControllerMode.MovementMode;

	void Start()
	{
		weapon.gameObject.SetActive (controllerMode == ControllerMode.AttackingMode);
	}

	void SwitchMode()
	{
		if (controllerMode == ControllerMode.AttackingMode) {
			controllerMode = ControllerMode.MovementMode;
		} else {
			controllerMode = ControllerMode.AttackingMode;
		}

		weapon.gameObject.SetActive (controllerMode == ControllerMode.AttackingMode);
	}

	void FixedUpdate()
	{
		if (switchModeButton) {
			SwitchMode();
		}

		if (controllerMode == ControllerMode.MovementMode) {
			var verticalVector = cameraTransform.forward * vertical;
			var horizontalVector = cameraTransform.right * horizontal;

			var movementDirection = (verticalVector + horizontalVector).normalized;

			body.transform.LookAt (body.transform.position + movementDirection * 10.0f);
			body.AddForce (movementDirection * speed, ForceMode.Acceleration);
		} else {

			body.transform.Rotate(0, horizontal * rotationSpeed * Time.deltaTime, 0);

			if (shootButton && weapon != null) {
				weapon.Fire (1.0f);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		horizontal = Input.GetAxis (horizontalAxisName);
		vertical = Input.GetAxis (verticalAxisName);
		shootButton = Input.GetButtonUp (shootButtonName);
		switchModeButton = Input.GetButtonUp (switchModeButtonName);
	}
}
