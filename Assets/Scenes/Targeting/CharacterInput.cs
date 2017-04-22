using UnityEngine;

public class CharacterInput : MonoBehaviour {

//	public CharacterController controller;

	public string rotationAxisName;
	public string forwardAxisName;

	public float forwardSpeed = 10.0f;
	public float backwardSpeed = 3.0f;

	public float rotationSpeed = 2.0f;

	public Transform characterModel;

	public Rigidbody body;

	float forward;
	float rotation;

	void FixedUpdate()
	{
		float speed = forwardSpeed;

		if (forward < 0)
			speed = backwardSpeed;

		characterModel.Rotate(0, rotation * rotationSpeed, 0);

		//		body.AddRelativeTorque(

		//		var forwardVector = ;
//		Vector3 forwardVector = transform.TransformDirection(Vector3.forward);

		body.AddRelativeForce(Vector3.forward * forward * speed, ForceMode.Acceleration);

		//		characterModel.Translate (forwardVector * forward * speed * Time.deltaTime);
		//		controller.Move(forwardVector * forward * speed * Time.deltaTime);
	}

	public void Update()
	{
		// controller.mov

		rotation = Input.GetAxis (rotationAxisName);
		forward = Input.GetAxis (forwardAxisName);

	}
}
