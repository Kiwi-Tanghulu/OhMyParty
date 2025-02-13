using System;
using OMG.Inputs;
using OMG.Interacting;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerHand : NetworkBehaviour, IHolder
    {
        [SerializeField] PlayInputSO input = null;

        [SerializeField] Transform holdingParent = null;
        public Transform HoldingParent => holdingParent;
        public GameObject HolderObject => gameObject;

        private IHoldable holdingObject = null;
        public IHoldable HoldingObject => holdingObject;

        private PlayerFocuser focuser = null;

        public bool IsEmpty => holdingObject == null;
        private bool active = true;

        public override void OnNetworkSpawn()
        {
            if(IsOwner == false)
                return;

            focuser = GetComponent<PlayerFocuser>();
            input.OnActiveEvent += HandleActive;
        }

        public override void OnNetworkDespawn()
        {
            if(IsOwner == false)
                return;

            input.OnActiveEvent -= HandleActive;
        }

        public bool Hold(IHoldable target, Vector3 point = default)
        {
            bool result = target.Hold(this, point);
            if(result)
                holdingObject = target;

            return result;
        }

        public void Active()
        {
            if(IsEmpty)
                return;
            
            holdingObject.Active();
        }

        public IHoldable Release()
        {
            IHoldable prevObject = holdingObject;

            holdingObject.Release();
            holdingObject = null;

            return prevObject;
        }

        public void SetActive(bool active)
        {
            this.active = active;
            if(this.active == false)
            {
                if (IsEmpty)
                    return;

                Release();
            }
        }

        private void HandleActive()
        {
            if (active == false)
                return;

            Active();
        }
    }
}
