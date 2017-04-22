using UnityEngine;

public class CharacterInputCamera : MonoBehaviour {

	public string horizontalAxisName;
	public string verticalAxisName;

	float vertical;
	float horizontal;

	public Transform cameraTransform;

	public Rigidbody body;

	public float speed = 10.0f;

	void FixedUpdate()
	{
		var verticalVector = cameraTransform.forward * vertical;
		var horizontalVector = cameraTransform.right * horizontal;

		var movementDirection = (verticalVector + horizontalVector).normalized;

		body.transform.LookAt(body.transform.position + movementDirection * 10.0f);
		body.AddForce(movementDirection * speed, ForceMode.Acceleration);
	}
	
	// Update is called once per frame
	void Update () {
		horizontal = Input.GetAxis (horizontalAxisName);
		vertical = Input.GetAxis (verticalAxisName);
	}
}
