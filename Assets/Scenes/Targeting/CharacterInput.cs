using UnityEngine;

public class CharacterInput : MonoBehaviour {

	public CharacterController controller;

	public string rotationAxisName;
	public string forwardAxisName;

	public float forwardSpeed = 10.0f;
	public float backwardSpeed = 3.0f;

	public float rotationSpeed = 2.0f;

	public Transform characterModel;

	public void Update()
	{
		// controller.mov

		float rotation = Input.GetAxis (rotationAxisName);
		float forward = Input.GetAxis (forwardAxisName);

		float speed = forwardSpeed;

		if (forward < 0)
			speed = backwardSpeed;

		characterModel.Rotate(0, rotation * rotationSpeed, 0);

//		var forwardVector = ;
		Vector3 forwardVector = transform.TransformDirection(Vector3.forward);
		controller.Move(forwardVector * forward * speed * Time.deltaTime);
	}
}
