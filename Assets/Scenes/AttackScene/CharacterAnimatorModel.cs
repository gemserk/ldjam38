using UnityEngine;

public class CharacterAnimatorModel : CharacterModel
{
	public Animator animator;

	public string damageAnimation;

	public GameObject deathEffectPrefab;

	public GameObject model;

	public override void DamageReceived()
	{
		if (animator == null)
			return;
		animator.Play (damageAnimation);
	}

	#region implemented abstract members of CharacterModel

	public override void Death ()
	{
		if (deathEffectPrefab == null)
			return;
		var deathEffect = GameObject.Instantiate (deathEffectPrefab);
		deathEffect.transform.position = transform.position;

		model.SetActive (false);
	}

	#endregion
}