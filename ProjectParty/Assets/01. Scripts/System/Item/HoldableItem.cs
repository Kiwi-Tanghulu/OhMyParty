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

        private TransformController transformController = null;
        private OwnershipController ownershipController = null;

        public event Action<bool> OnHoldEvent = null;

        public virtual bool Hold(IHolder holder, Vector3 point = default)
        {
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
            OnHoldEvent?.Invoke(true);

            return true;
        }

        public IHolder Release()
        {
            IHolder prevHolder = currentHolder;
            currentHolder = null;
            
            transformController.SetParent(null);
            
            OnRelease();
            OnHoldEvent?.Invoke(false);

            return prevHolder;
        }

        public abstract void OnHold();
        public abstract void OnRelease();
    }
}
