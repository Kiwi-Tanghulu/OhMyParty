using System;
using OMG.Interacting;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class Rock : NetworkBehaviour, IHoldable
    {
        private IHolder currentHolder = null;
        public IHolder CurrentHolder => currentHolder;

        private RockCollision collision = null;

        public event Action<bool> OnHoldEvent = null;

        private void Awake()
        {
            collision = GetComponent<RockCollision>();
        }

        /// <summary>
        /// Only Host Could Call this Method
        /// </summary>
        public virtual void Init()
        {
            collision.Init();
        }

        public bool Hold(IHolder holder, Vector3 point = default)
        {
            if(holder.IsEmpty == false)
                return false;

            if(currentHolder != null)
                return false;

            currentHolder = holder;
            transform.SetParent(currentHolder.HoldingParent);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            OnHoldEvent?.Invoke(true);

            collision.ActiveRigidbody(false);

            return true;
        }

        public IHolder Release()
        {
            transform.SetParent(null);
            OnHoldEvent?.Invoke(false);

            collision.ActiveRigidbody(true);

            IHolder prevHolder = currentHolder;
            currentHolder = null;

            return prevHolder;
        }
    }
}
