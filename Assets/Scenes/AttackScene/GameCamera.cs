using UnityEngine;

public class GameCamera : MonoBehaviour
{
	public Transform cameraTransform;

	public void CenterOn (Transform transform)
	{
		cameraTransform.position = transform.position;
	}
	
}
