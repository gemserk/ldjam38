using System.Collections.Generic;
using System.Linq;
using Gemserk.LD38.Game.World;
using Gemserk.Utils;
using UnityEngine;

public class WorldMovementBlocks : WorldMovement
{
	public Vector3 unitVector = new Vector3(1, 1, 1);

	#region implemented abstract members of WorldMovement
	public override Vector3 Move (Vector3 currentPosition, Vector3 direction, int heightTolerance)
	{
		var newPosition = currentPosition + new Vector3 (unitVector.x * direction.x, 0, unitVector.z * direction.z);

		//newPosition += new Vector3 (0, unitVector.y * (heightTolerance + 1), 0);


	    var cubesInNewPosition = Util.GetWorldCubesInColumn(newPosition);

	    var firstCube = cubesInNewPosition.FirstOrDefault();

	    if (firstCube == null)
	    {
	        return currentPosition;
	    }

	    var topPosition = firstCube.transform.position;

	    newPosition = topPosition;// + Vector3.up;

	    return newPosition;
	}
	#endregion


}