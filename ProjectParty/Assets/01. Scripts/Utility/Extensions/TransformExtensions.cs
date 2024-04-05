using UnityEngine;

public static class TransformExtensions
{
	public static bool InnerDistance(this Transform left, Transform right, float distance)
    {
        float sqrDistance = (left.position - right.position).sqrMagnitude;
        return sqrDistance < (distance * distance);
    }
}
