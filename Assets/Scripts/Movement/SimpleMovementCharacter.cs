using UnityEngine;

public class SimpleMovementCharacter : MonoBehaviour
{
	public GameObject character;

	public WorldMovement worldMovement;

	public int heightTolerance = 1;

	public void MoveLeft ()
	{
		character.transform.position = worldMovement.Move(character.transform.position, character.transform.right, heightTolerance);
	}

	public void MoveRight()
	{
		character.transform.position = worldMovement.Move(character.transform.position, character.transform.right * -1, heightTolerance);
	}

	public void MoveForward()
	{
		character.transform.position = worldMovement.Move(character.transform.position, character.transform.forward, heightTolerance);
	}

	public void MoveBackwards()
	{
		character.transform.position = worldMovement.Move(character.transform.position, character.transform.forward * -1, heightTolerance);
	}
	
}
