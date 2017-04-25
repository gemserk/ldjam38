using UnityEngine;

public abstract class CharacterModel : MonoBehaviour
{
	public abstract void DamageReceived();

	public abstract void Death();
}