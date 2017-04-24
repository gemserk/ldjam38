using System.Collections.Generic;
using System.Linq;
using Gemserk.LD38.Game.World;
using UnityEngine;

public class WorldMovementBlocks : WorldMovement
{
	public Vector3 unitVector = new Vector3(1, 1, 1);

	#region implemented abstract members of WorldMovement
	public override Vector3 Move (Vector3 currentPosition, Vector3 direction, int heightTolerance)
	{
		var newPosition = currentPosition + new Vector3 (unitVector.x * direction.x, 0, unitVector.z * direction.z);

		//newPosition += new Vector3 (0, unitVector.y * (heightTolerance + 1), 0);


	    var cubesInNewPosition = GetWorldCubesInColumn(newPosition);

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

    public static IEnumerable<GameObject> GetWorldCubesInColumn(Vector3 position)
    {
        var axis = Vector3.down;
        Ray ray = new Ray(position - axis * 1000, axis);

        var hits = Physics.RaycastAll(ray);

        return hits.OrderBy(hit => hit.distance)
            .Select(hit => hit.collider.gameObject.GetComponentInParent<WorldCube>())
            .Where(worldCube => worldCube != null)
            .Select(worldCube => worldCube.gameObject);

    }
}