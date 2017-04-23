using UnityEngine;

public class SimpleMovementCharacter : MonoBehaviour
{
	public GameObject character;

	public Vector3 movementVector;

	// TODO: check nearby blocks and height difference and move there...
	// TODO: use character looking direction too to consider left/right

	public void MoveLeft ()
	{
		character.transform.position += new Vector3 (movementVector.x, 0, 0);
	}

	public void MoveRight()
	{
		character.transform.position += new Vector3 (-movementVector.x, 0, 0);
	}

	public void MoveForward()
	{
		character.transform.position += new Vector3 (0, 0, movementVector.z);
	}

	public void MoveBackwards()
	{
		character.transform.position += new Vector3 (0, 0, -movementVector.z);
	}
	
}
