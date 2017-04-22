using UnityEngine;

public class WorldCreator
{
	public void CreateWorld(Transform parent, GameObject cubePrefab, Vector3 worldSize)
	{
		var collider = cubePrefab.GetComponent<BoxCollider> ();
		var cubeSize = collider.size;

		float x = (-worldSize.x * cubeSize.x) + (cubeSize.x * 0.5f);
		float y = (-worldSize.y * cubeSize.y) + (cubeSize.y * 0.5f);
		float z = (-worldSize.z * cubeSize.z) + (cubeSize.z * 0.5f);

		for (int i = 0; i < worldSize.x; i++) {
			y = (-worldSize.y * cubeSize.y) + (cubeSize.y * 0.5f);
			for (int j = 0; j < worldSize.y; j++) {
				z = (-worldSize.z * cubeSize.z) + (cubeSize.z * 0.5f);
				for (int k = 0; k < worldSize.z; k++) {
					var cubeObject = GameObject.Instantiate (cubePrefab, parent);
					cubeObject.transform.localPosition = new Vector3 (x, y, z);
					z += cubeSize.z;
				}
				y += cubeSize.y;
			}
			x += cubeSize.x;
		}
	}
}

public class WorldCreatorBehaviour : MonoBehaviour {

	public GameObject cubePrefab;

	[Tooltip("No sea nabo, es vector de enteros")]
	public Vector3 worldSize;

	// Use this for initialization
	void Start () {
		var worldCreator = new WorldCreator ();
		worldCreator.CreateWorld (this.transform, cubePrefab, worldSize);	
	}

}
