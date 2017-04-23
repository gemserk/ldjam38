using UnityEngine;

public class CameraRailInput : MonoBehaviour {

	public GameCamera gameCamera;

	public string horizontalAxisName;

	public float speedHorizontal = 15;

	public void Update()
	{
		float horizontal = Input.GetAxis (horizontalAxisName);
		gameCamera.MoveRailPosition (horizontal * speedHorizontal * Time.deltaTime);
	}

}
