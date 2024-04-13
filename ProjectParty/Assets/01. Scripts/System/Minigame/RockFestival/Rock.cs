using System;
using OMG.Interacting;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class Rock : NetworkBehaviour, IHoldable, IFocusable
    {
        private IHolder currentHolder = null;
        public IHolder CurrentHolder => currentHolder;

        public GameObject CurrentObject => gameObject;

        private RockTransform rockTransform = null;
        private RockCollision collision = null;

        public event Action<bool> OnHoldEvent = null;

        protected virtual void Awake()
        {
            collision = GetComponent<RockCollision>();
            rockTransform = GetComponent<RockTransform>();
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
            // rockTransform.SetPositionImmediately(currentHolder.HoldingParent.position);
            rockTransform.SetParent(currentHolder.HolderObject.GetComponent<NetworkObject>().OwnerClientId, currentHolder);
            OnHoldEvent?.Invoke(true);

            collision.SetActiveRigidbody(false);

            return true;
        }

        public void Active()
        {
            Vector3 direction = currentHolder.HoldingParent.forward + Vector3.up * 0.5f;
            collision.SetActiveCollisionOther(true);
            collision.SetActiveRigidbody(true);
            collision.AddForce(direction, 12.5f);

            currentHolder.Release();
        }

        public IHolder Release()
        {
            IHolder prevHolder = currentHolder;
            currentHolder = null;
            
            rockTransform.ReleaseParent();

            // if(collision.ActiveCollisionOther == false)
            //     rockTransform.FitToGround();
            collision.SetActiveRigidbody(true);
            
            OnHoldEvent?.Invoke(false);

            return prevHolder;
        }

        public void OnFocusBegin(Vector3 point)
        {
            
        }

        public void OnFocusEnd()
        {
            
        }
    }
}
