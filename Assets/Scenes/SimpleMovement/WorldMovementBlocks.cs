using UnityEngine;

public class WorldMovementBlocks : WorldMovement
{
	public Vector3 unitVector = new Vector3(1, 1, 1);

	public float boxChecksize = 0.2f;

	#region implemented abstract members of WorldMovement
	public override Vector3 Move (Vector3 currentPosition, Vector2 direction, int heightTolerance)
	{
		var newPosition = currentPosition + new Vector3 (unitVector.x * direction.x, 0, unitVector.y * direction.y);

//		if (Physics.CheckBox(newPosition - new Vector3(0, unitVector.y, 0), unitVector * 0.5f)) {
		if (Physics.CheckBox(newPosition, unitVector * boxChecksize)) {
			return newPosition;
		}

		return currentPosition;
	}
	#endregion
}