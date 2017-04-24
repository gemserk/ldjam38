using UnityEngine;

public class ProjectileTrail : MonoBehaviour {

	public GameObject trailPrefab;

	void Start()
	{
		if (trailPrefab != null)
			GameObject.Instantiate (trailPrefab, this.transform);
	}

}
