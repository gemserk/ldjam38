using UnityEngine;

public class CharacterAnimatorModel : CharacterModel
{
	public Animator animator;

	public string damageAnimation;

	public override void DamageReceived()
	{
		if (animator == null)
			return;
		animator.Play (damageAnimation);
	}

}