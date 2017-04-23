using UnityEngine;

public class GameCamera : MonoBehaviour
{
	public Transform cameraTransform;

	bool transitioning;

	Vector3 targetPosition;

	public float transitionSpeed = 10.0f;

	public CameraInput cameraInput;

	public AnimationCurve movementCurve;

	public bool IsTransitioning()
	{
		return transitioning;
	}

	public void CenterOn (Transform transform)
	{
		targetPosition = transform.position;
		transitioning = true;

		if (cameraInput != null)
			cameraInput.enabled = false;
	}

	void LateUpdate()
	{
		if (!transitioning)
			return;
		
		cameraTransform.position = Vector3.Lerp (cameraTransform.position, targetPosition, movementCurve.Evaluate(Time.deltaTime * transitionSpeed));
		transitioning = Vector3.Distance (cameraTransform.position, targetPosition) > 1.0f;

		if (!transitioning) {
			if (cameraInput != null)
				cameraInput.enabled = true;
		}
	}
	
}
