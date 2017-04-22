using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInput : MonoBehaviour {

	public string horizontalAxisName;
	public string verticalAxisName;

	public Transform targetTransform;

	public float speedHorizontal = -1;
	public float speedVertical = -1;

	public void Update()
	{
		float horizontal = Input.GetAxis (horizontalAxisName);
		float vertical = Input.GetAxis (verticalAxisName);

		Vector3 position = targetTransform.localPosition;

		Vector3 right = targetTransform.right * horizontal * speedHorizontal * Time.deltaTime;
		Vector3 forward = targetTransform.forward * vertical * speedVertical * Time.deltaTime;

		var newPosition = position + right + forward;

		targetTransform.localPosition = newPosition;

		// var newPosition = position + 
	}

}
