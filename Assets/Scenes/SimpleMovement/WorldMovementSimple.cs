using UnityEngine;

public class WorldMovementSimple : WorldMovement
{
	public Vector3 movementVector = new Vector3(1, 1, 1);

	#region implemented abstract members of WorldMovement

	public override Vector3 Move (Vector3 currentPosition, Vector3 direction, int heightTolerance)
	{
		return currentPosition + new Vector3 (movementVector.x * direction.x, 0, movementVector.z * direction.z);
	}

	#endregion
	
}