using System.Linq;
using Gemserk.Utils;
using UnityEngine;

public class SimpleMovementCharacter : MonoBehaviour
{
	public Character character;

	public WorldMovement worldMovement;

	public float deathTorque = 1;

	public void MoveLeft ()
	{
	    if (!character.IsDead)
	    {
	        character.transform.position = worldMovement.Move(character.transform.position, character.transform.right,
	            0);
	    }
	}

	public void MoveRight()
	{
		if (!character.IsDead && worldMovement != null)
	    {
	        character.transform.position = worldMovement.Move(character.transform.position, character.transform.right * -1,
	            0);
	    }
	}

	public void MoveForward()
	{
		if (!character.IsDead && worldMovement != null)
	    {
	        character.transform.position = worldMovement.Move(character.transform.position, character.transform.forward,
	            0);
	    }
	}

	public void MoveBackwards()
	{
		if (!character.IsDead && worldMovement != null)
	    {
	        character.transform.position = worldMovement.Move(character.transform.position,
	            character.transform.forward * -1, 0);
	    }
	}

    public void Update()
    {
		if (!character.IsDead && worldMovement != null)
        {
            var currentPosition = character.transform.position;
            var newPosition = worldMovement.Move(currentPosition, Vector3.zero, 0);
            if (Vector3.Distance(currentPosition, newPosition) < 0.1f)
            {
                var cubesUnder = Util.GetWorldCubesInColumn(newPosition)
                    .Where(go => go.transform.position.y < newPosition.y + 0.2f);
                if (!cubesUnder.Any())
                {
                    var rigidBody = character.GetComponent<Rigidbody>();
                    rigidBody.isKinematic = false;
                    character.IsDead = true;
                    rigidBody.AddRelativeTorque(Random.insideUnitSphere * deathTorque, ForceMode.Acceleration);
                }
            }

            character.transform.position = newPosition;

        }
    }

}
