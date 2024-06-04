using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitableObject : MonoBehaviour, IDamageable
{
    public UnityEvent<Vector3/*hit point*/> OnDamagedEvent;

    public void OnDamaged(float damage, Transform attacker, Vector3 point, Vector3 normal = default)
    {
        OnDamagedEvent?.Invoke(point);
    }
}
