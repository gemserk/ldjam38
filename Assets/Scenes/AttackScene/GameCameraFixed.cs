using UnityEngine;

public class GameCameraFixed : GameCamera
{
	public Transform cameraTransform;

	bool transitioning;

	Vector3 targetPosition;

	public float transitionSpeed = 10.0f;

	public AnimationCurve movementCurve;

	public Transform railStartPosition;
	public Transform railEndPosition;

	public float startingRailPosition = 0.1f;

	float currentRailPosition;

	void Start()
	{
		SetRailPosition (startingRailPosition);
	}

	public override bool IsTransitioning()
	{
		return transitioning;
	}

	public void CenterOn (Transform transform)
	{
		targetPosition = transform.position;
		transitioning = true;
	}

	public override void SetRailPosition(float t)
	{
		targetPosition = Vector3.Lerp (railStartPosition.transform.position, railEndPosition.transform.position, t);
		transitioning = true;
		currentRailPosition = t;
	}

	public override void MoveRailPosition(float direction)
	{
		currentRailPosition = Mathf.Clamp (currentRailPosition + direction, 0, 1);
		SetRailPosition (currentRailPosition);
	}

	void LateUpdate()
	{
		if (!transitioning)
			return;
		
		cameraTransform.position = Vector3.Lerp (cameraTransform.position, targetPosition, movementCurve.Evaluate(Time.deltaTime * transitionSpeed));
		transitioning = Vector3.Distance (cameraTransform.position, targetPosition) > 1.0f;
	}
	
}
