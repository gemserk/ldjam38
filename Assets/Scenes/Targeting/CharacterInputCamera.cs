using UnityEngine;
using Assets.Scripts.Game.Weapons;
using UnityEngine.Serialization;

public class CharacterInputCamera : MonoBehaviour {

	public enum ControllerMode
	{
		MovementMode,
		AttackingMode
	}

	public string horizontalAxisName;
	public string verticalAxisName;

	[FormerlySerializedAs("shootButtonName")]
	public string actionButtonName;

	public string switchModeButtonName;

	float vertical;
	float horizontal;

	public Transform cameraTransform;

	public Rigidbody body;

	public float speed = 10.0f;
	public float rotationSpeed = 10.0f;
	public float weaponRotationSpeed = -10.0f;

	public AnimationCurve chargeCurve;

	bool shootButton;
	bool switchModeButton;

	bool charging;
	float charge;

	[Range(0.1f, 100.0f)]
	public float chargeTime = 1.0f;

	float chargeSpeed;

	public Weapon weapon;

	// weapon rotation limits
	// weapon starts in fixed rotation (between turn and turn)
	// jump

	bool isJumping;

	public float jumpStartImpulse;
	public float jumpSpeedMultiplier = 0.25f;
	public FloorDetection floorDetection;

	public ControllerMode controllerMode = ControllerMode.MovementMode;

	void Start()
	{
		weapon.gameObject.SetActive (controllerMode == ControllerMode.AttackingMode);
	}

	void SwitchMode()
	{
		if (controllerMode == ControllerMode.AttackingMode) {
			controllerMode = ControllerMode.MovementMode;
		} else if (controllerMode == ControllerMode.MovementMode) {
			controllerMode = ControllerMode.AttackingMode;
		}

		weapon.gameObject.SetActive (controllerMode == ControllerMode.AttackingMode);
	}
		
	// Update is called once per frame
	void Update () {

		if (chargeTime > 0)
			chargeSpeed = 1.0f / chargeTime;

		horizontal = Input.GetAxis (horizontalAxisName);
		vertical = Input.GetAxis (verticalAxisName);
		switchModeButton = Input.GetButtonUp (switchModeButtonName);

		shootButton = Input.GetButton(actionButtonName);

		if (switchModeButton) {
			SwitchMode();
		}

		if (controllerMode == ControllerMode.MovementMode) {
			if (charging)
				return;

			float speedMultiplier = floorDetection.IsOnFloor() ? 1.0f : jumpSpeedMultiplier;
			
			var verticalVector = cameraTransform.forward * -1 * vertical;
			var horizontalVector = cameraTransform.right * horizontal;

			var movementDirection = (verticalVector + horizontalVector).normalized;

			body.transform.LookAt (body.transform.position + movementDirection * 10.0f);
			body.AddForce (movementDirection * speed * speedMultiplier, ForceMode.Acceleration);

			if (!isJumping && shootButton && floorDetection.IsOnFloor()) {
				isJumping = true;
				body.AddForce (Vector3.up * body.mass * jumpStartImpulse, ForceMode.Impulse);
			} else if (isJumping && !shootButton) {
				isJumping = false;
			}

		} else {

			if (!charging) {
				body.transform.Rotate (0, horizontal * rotationSpeed * Time.deltaTime, 0);
				weapon.transform.Rotate (vertical * weaponRotationSpeed * Time.deltaTime, 0, 0);
			}

			if (weapon != null) {

				if (charging) {
					charge += Time.deltaTime * chargeSpeed;
					charge = Mathf.Clamp (charge, 0.0f, 1.0f);
				}

				if (!charging && shootButton) {
					charging = true;
					charge = 0.0f;
				} else if (charging && !shootButton) {
					charging = false;
					weapon.Fire (chargeCurve.Evaluate(charge));
				}
					
			}
		}
	}
}
