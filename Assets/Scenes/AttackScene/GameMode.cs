using UnityEngine;
using Assets.Scripts.Game.Weapons;

public abstract class GameMode : MonoBehaviour
{
	public abstract void OnCharacterFired (Character character);
}
