using OMG.Ragdoll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerRagdollController : RagdollController
    {
        protected override void Awake()
        {
            onInitActive = true;

            base.Awake();

            SetActive(false);
        }

        public override void SetActive(bool value)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].Col.enabled = value;
                parts[i].Rb.isKinematic = !value;
            }

            if (value)
            {
                for (int i = 0; i < parts.Length; i++)
                {
                    parts[i].Rb.velocity = Vector3.zero;
                    parts[i].Rb.angularVelocity = Vector3.zero;
                }

                OnActiveEvent?.Invoke();
            }
            else
            {
                OnDeactiveEvent?.Invoke();
            }
        }
    }
}
