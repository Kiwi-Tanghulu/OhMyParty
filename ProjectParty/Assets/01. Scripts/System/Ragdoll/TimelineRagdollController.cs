using OMG.NetworkEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace OMG.Ragdoll
{
    public class TimelineRagdollController : RagdollController
    {
        public void SetActive(bool value)
        {
            base.SetActive(value);
        }

        public void AddForce(float power)
        {
            AddForce(new AttackParams()
            {
                Damage = power,
                Dir = transform.forward,
            });
        }
    }
}