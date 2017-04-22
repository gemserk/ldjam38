using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraLookEditor2 : MonoBehaviour {

	public Camera camera;
	public Transform followObject;

	// Update is called once per frame
	void Update () {
		if (camera == null)
			return;
		if (followObject == null)
			return;
		camera.transform.position = followObject.position;
	}
}