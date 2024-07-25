using OMG.NetworkEvents;
using OMG.Ragdoll;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerRagdollController : RagdollController
    {
        public override void Init(CharacterController controller)
        {
            onInitActive = true;

            base.Init(controller);

            SetActive(false);
        }

        protected override void SetActive(BoolParams value)
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
