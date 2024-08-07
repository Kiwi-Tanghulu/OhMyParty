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
            SetActive(value, false);
        }

        public void AddForce(float power)
        {
            AddForce(power, transform.forward);
        }
    }
}