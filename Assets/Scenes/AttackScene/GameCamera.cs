using UnityEngine;

public class GameCamera : MonoBehaviour
{
	public Transform cameraTransform;

	bool transitioning;

	Vector3 targetPosition;

	public float transitionSpeed = 10.0f;

	public CameraInput cameraInput;

	public AnimationCurve movementCurve;

	public Transform railStartPosition;
	public Transform railEndPosition;

	public float startingRailPosition = 0.1f;

	float currentRailPosition;

	void Start()
	{
		SetRailPosition (startingRailPosition);
	}

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

	public void SetRailPosition(float t)
	{
		targetPosition = Vector3.Lerp (railStartPosition.transform.position, railEndPosition.transform.position, t);
		transitioning = true;
		currentRailPosition = t;
	}

	public void MoveRailPosition(float direction)
	{
		if (Mathf.Abs (direction) < 0.01f)
			return;
		currentRailPosition = Mathf.Clamp (currentRailPosition + direction, 0, 1);
		SetRailPosition (currentRailPosition);
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
