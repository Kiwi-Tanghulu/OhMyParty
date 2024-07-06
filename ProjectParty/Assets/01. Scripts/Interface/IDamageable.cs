using OMG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void OnDamaged(float damage, Transform attacker, Vector3 point,
        HitEffectType effectType, Vector3 normal = default);
}
