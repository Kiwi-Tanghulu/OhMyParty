using UnityEngine;

public struct HitInfo
{
    public Collider collider;
    public Vector3 hitPoint;
}
public interface IPlayerCollision
{
    public void OnCollision(HitInfo hitInfo);
}
