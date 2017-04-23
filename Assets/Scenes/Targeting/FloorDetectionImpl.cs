using UnityEngine;

public class FloorDetectionImpl : FloorDetection {

	int floorContact;

	public override bool IsOnFloor()
	{
		return floorContact > 0;
	}

//	void OnCollisionEnter(Collision collision)
//	{
//		floorContact++;
//	}
//		
//	void OnCollisionExit(Collision collision)
//	{
//		floorContact--;
//	}

	void OnTriggerEnter(Collider other) {
		floorContact++;
	}

	void OnTriggerExit(Collider other) {
		floorContact--;
	}

}
