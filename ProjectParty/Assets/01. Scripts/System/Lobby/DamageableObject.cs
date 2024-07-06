using OMG;
using OMG.NetworkEvents;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

using NetworkEvent = OMG.NetworkEvents.NetworkEvent;

public class DamageableObject : MonoBehaviour, IDamageable
{
    public UnityEvent<Vector3/*hit point*/> OnDamagedEvent;

    private NetworkEvent<Vector3Params> onHitEvent;

    private void Start()
    {
        onHitEvent = new NetworkEvent<Vector3Params>($"{transform.name}onHitEvent");

        onHitEvent.AddListener(OnDamagedClientRpc);

        onHitEvent.Register(transform.root.GetComponent<NetworkObject>());
    }

    public void OnDamaged(float damage, Transform attacker, Vector3 point,
        HitEffectType effectType, Vector3 normal = default)
    {
        Vector3Params param = new Vector3Params { Value = point };

        onHitEvent.Broadcast(param);
    }

    private void OnDamagedClientRpc(Vector3Params point)
    {
        OnDamagedEvent?.Invoke(point.Value);
    }
}
