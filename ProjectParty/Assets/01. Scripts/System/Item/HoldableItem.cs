using System;
using OMG.Interacting;
using OMG.Utility.Netcodes;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Items
{
    public abstract class HoldableItem : NetworkItem, IHoldable
    {
        protected IHolder currentHolder = null;
        public IHolder CurrentHolder => currentHolder;

        protected TransformController transformController = null;
        protected OwnershipController ownershipController = null;

        public event Action<bool> OnHoldEvent = null;
        public ulong HolderID { get; private set; } = ulong.MaxValue;

        protected override void Awake()
        {
            base.Awake();

            transformController = GetComponent<TransformController>();
            ownershipController = GetComponent<OwnershipController>();
        }

        public virtual bool Hold(IHolder holder, Vector3 point = default)
        {
            if (HolderID != ulong.MaxValue)
                return false;

            if(holder.IsEmpty == false)
                return false;

            if(currentHolder != null)
                return false;

            currentHolder = holder;
            NetworkObject holderObject = currentHolder.HolderObject.GetComponent<NetworkObject>();
            ownershipController.SetOwner(holderObject.OwnerClientId, () => {
                transformController.SetPositionImmediately(currentHolder.HoldingParent.position);
                transformController.SetParent(currentHolder.HoldingParent);
            });

            OnHold();
            HoldServerRpc(holderObject.OwnerClientId);
            OnHoldEvent?.Invoke(true);

            return true;
        }

        public IHolder Release()
        {
            IHolder prevHolder = currentHolder;
            currentHolder = null;
            
            transformController.SetParent(null);
            
            OnRelease();
            HoldServerRpc(ulong.MaxValue);
            OnHoldEvent?.Invoke(false);

            return prevHolder;
        }

        public abstract void OnHold();
        public abstract void OnRelease();

        [ServerRpc(RequireOwnership = false)]
        private void HoldServerRpc(ulong holderID)
        {
            if(HolderID != ulong.MaxValue && holderID != HolderID)
                return;

            HoldClientRpc(holderID);
        }

        [ClientRpc]
        private void HoldClientRpc(ulong holderID)
        {
            HolderID = holderID;
        }
    }
}
