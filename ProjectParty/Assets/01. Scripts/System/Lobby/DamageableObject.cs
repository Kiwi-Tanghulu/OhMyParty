using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class DamageableObject : NetworkBehaviour, IDamageable
{
    public UnityEvent<Vector3/*hit point*/> OnDamagedEvent;

    public void OnDamaged(float damage, Transform attacker, Vector3 point, Vector3 normal = default)
    {
        OnDamagedClientRpc(point);
    }

    [ServerRpc]
    private void OnDamagedServerRpc()
    {

    }

    [ClientRpc]
    private void OnDamagedClientRpc(Vector3 point)
    {
        OnDamagedEvent?.Invoke(point);
    }
}
