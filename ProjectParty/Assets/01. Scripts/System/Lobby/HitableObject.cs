using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitableObject : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform damagedTransform;
    public UnityEvent<Transform/*DamagedTransform*/> OnDamagedEvent;

    private void Start()
    {
        if(damagedTransform == null)
            damagedTransform = transform;
    }

    public Transform GetDamagedTransfrom()
    {
        return damagedTransform;
    }

    public void OnDamaged(float damage, Transform attacker, Vector3 point, Vector3 normal = default)
    {
        OnDamagedEvent?.Invoke(GetDamagedTransfrom());
    }
}
