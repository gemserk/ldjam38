using UnityEngine;

public class RandomMaterialConfigurer : MonoBehaviour {

	public Material[] materials;

	public Renderer objectRenderer;

	// Use this for initialization
	void Start () {
		if (materials.Length == 0)
			return;
		if (objectRenderer == null)
			return;
		objectRenderer.sharedMaterial = materials[UnityEngine.Random.Range(0, materials.Length)];
	}

}
