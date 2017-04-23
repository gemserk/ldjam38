using UnityEngine;

public class CameraRailInput : CameraInput {

	public GameCamera gameCamera;

	public string horizontalAxisName;

	public float speedHorizontal = 1;

	public void Update()
	{
		float horizontal = Input.GetAxis (horizontalAxisName);

		if (Mathf.Abs (horizontal) < 0.01f)
			return;
		gameCamera.MoveRailPosition (horizontal * speedHorizontal * Time.deltaTime);
	}

}
