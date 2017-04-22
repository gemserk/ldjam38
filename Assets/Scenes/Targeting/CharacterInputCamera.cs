using UnityEngine;
using Assets.Scripts.Game.Weapons;

public class CharacterInputCamera : MonoBehaviour {

	public string horizontalAxisName;
	public string verticalAxisName;
	public string shootButtonName;

	float vertical;
	float horizontal;

	public Transform cameraTransform;

	public Rigidbody body;

	public float speed = 10.0f;

	bool shootButton;

	public Weapon weapon;

	void FixedUpdate()
	{
		var verticalVector = cameraTransform.forward * vertical;
		var horizontalVector = cameraTransform.right * horizontal;

		var movementDirection = (verticalVector + horizontalVector).normalized;

		body.transform.LookAt(body.transform.position + movementDirection * 10.0f);
		body.AddForce(movementDirection * speed, ForceMode.Acceleration);
	
		if (shootButton && weapon != null) {
			weapon.Fire (1.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		horizontal = Input.GetAxis (horizontalAxisName);
		vertical = Input.GetAxis (verticalAxisName);
		shootButton = Input.GetButtonUp (shootButtonName);
	}
}
