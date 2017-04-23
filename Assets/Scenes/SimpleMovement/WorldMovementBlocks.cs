using UnityEngine;

public class WorldMovementBlocks : WorldMovement
{
	public Vector3 unitVector = new Vector3(1, 1, 1);

	public float boxChecksize = 0.2f;

	#region implemented abstract members of WorldMovement
	public override Vector3 Move (Vector3 currentPosition, Vector3 direction, int heightTolerance)
	{
		var newPosition = currentPosition + new Vector3 (unitVector.x * direction.x, 0, unitVector.z * direction.z);

		newPosition += new Vector3 (0, unitVector.y * (heightTolerance + 1), 0);

		for (int i = 0; i <= heightTolerance * 2; i++) {
			newPosition -= new Vector3 (0, unitVector.y, 0);

			if (Physics.CheckBox(newPosition, unitVector * boxChecksize)) {
				return newPosition;
			}
		}
			
		return currentPosition;
	}
	#endregion
}