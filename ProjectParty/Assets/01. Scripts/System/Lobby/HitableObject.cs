using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class HitableObject : NetworkBehaviour, IDamageable
{
    public UnityEvent<Vector3/*hit point*/> OnDamagedEvent;

    public void OnDamaged(float damage, Transform attacker, Vector3 point, Vector3 normal = default)
    {
        OnDamagedClientRpc(point);
    }

    [ClientRpc]
    private void OnDamagedClientRpc(Vector3 point)
    {
        OnDamagedEvent?.Invoke(point);
    }
}
