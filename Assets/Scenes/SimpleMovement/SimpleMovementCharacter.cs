using UnityEngine;

public class SimpleMovementCharacter : MonoBehaviour
{
	public GameObject character;

	// TODO: use character looking direction too to consider left/right

	public WorldMovement worldMovement;

	public int heightTolerance = 1;

	public void MoveLeft ()
	{
		character.transform.position = worldMovement.Move(character.transform.position, new Vector2(1, 0), heightTolerance);
//		character.transform.position += new Vector3 (movementVector.x, 0, 0);
	}

	public void MoveRight()
	{
		character.transform.position = worldMovement.Move(character.transform.position, new Vector2(-1, 0), heightTolerance);
//		character.transform.position += new Vector3 (-movementVector.x, 0, 0);
	}

	public void MoveForward()
	{
		character.transform.position = worldMovement.Move(character.transform.position, new Vector2(0, 1), heightTolerance);
//		character.transform.position += new Vector3 (0, 0, movementVector.z);
	}

	public void MoveBackwards()
	{
		character.transform.position = worldMovement.Move(character.transform.position, new Vector2(0, -1), heightTolerance);
//		character.transform.position += new Vector3 (0, 0, -movementVector.z);
	}
	
}
