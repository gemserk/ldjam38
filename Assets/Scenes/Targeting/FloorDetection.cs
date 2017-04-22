using UnityEngine;

public class FloorDetection : MonoBehaviour {

	int floorContact;

	public bool IsOnFloor()
	{
		return floorContact > 0;
	}

	void OnTriggerEnter(Collider other) {
		floorContact++;
	}

	void OnTriggerExit(Collider other) {
		floorContact--;
	}

}
