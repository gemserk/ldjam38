using UnityEngine;

public abstract class WorldMovement : MonoBehaviour
{
	//public class WorldBlock {
	//
	//}

//	public bool CanMoveHorizontal(WorldBlock block, int blockCount, int heightTolerance)
//	{
//		return false;
//	}

//	public abstract WorldBlock GetCurrentBlock(Vector3 position);

//	public abstract WorldBlock GetHorizontalBlock(WorldBlock block, int blockCount, int heightTolerance);

	public abstract Vector3 Move(Vector3 currentPosition, Vector2 direction, int heightTolerance);

}

// TODO: check nearby blocks and height difference and move there...