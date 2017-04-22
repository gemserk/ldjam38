using UnityEngine;

[ExecuteInEditMode]
public class TransformLookAtObject : MonoBehaviour {

	public Transform controlObject;
	public Transform followObject;

	// Update is called once per frame
	void Update () {
		if (controlObject == null)
			return;
		if (followObject == null)
			return;
		controlObject.transform.LookAt (followObject.position, followObject.up);
	}
}