using UnityEngine;

public class ChargeIndicator : MonoBehaviour {

	public Transform indicatorTransform;

	public float minScale = 0.0f;
	public float maxScale = 2.0f;

	public Gradient gradient;

	public Renderer indicatorRenderer;

	public void UpdateCharge (WeaponControl weaponControl)
	{
		float charge = weaponControl.GetCharge ();
		var weapon = weaponControl.GetWeapon ();

		var localScale = indicatorTransform.localScale;
		localScale.z = Mathf.Lerp (minScale, maxScale, charge);
		indicatorTransform.localScale = localScale;

		indicatorTransform.position = weaponControl.GetWeapon ().GetFireTransform ().position;
		indicatorTransform.forward = weaponControl.GetWeapon ().GetFireTransform ().forward;

		if (indicatorRenderer != null) {
			indicatorRenderer.material.color = gradient.Evaluate (charge);
		}

	}

}
