using UnityEngine;

public abstract class GameMode : MonoBehaviour
{
	public abstract void OnCharacterFired (Character character);

	public abstract void OnCharacterDeath (Character character);
}
