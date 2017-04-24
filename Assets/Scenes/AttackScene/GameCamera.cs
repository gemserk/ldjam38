using UnityEngine;

public abstract class GameCamera : MonoBehaviour
{
    public abstract bool IsTransitioning();
    public abstract void SetRailPosition(float t);
    public abstract void MoveRailPosition(float direction);
    public abstract void CenterOn(Vector3 position);
}