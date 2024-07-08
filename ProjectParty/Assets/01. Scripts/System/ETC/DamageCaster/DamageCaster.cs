using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace OMG
{
    public abstract class DamageCaster : MonoBehaviour
    {
        [SerializeField] protected Transform eyeTrm;
        [SerializeField] protected HitEffectType hitEffectType;

        public abstract RaycastHit[] DamageCast(Transform attacker);
    }
}