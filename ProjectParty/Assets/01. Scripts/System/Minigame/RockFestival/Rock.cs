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

        private RockCollision collision = null;

        public event Action<bool> OnHoldEvent = null;

        protected virtual void Awake()
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

            collision.SetActiveRigidbody(false);

            return true;
        }

        public void Active()
        {
            IHolder prevHolder = Release();
            prevHolder.Release();

            Vector3 direction = prevHolder.HoldingParent.forward + Vector3.up * 0.5f;
            collision.SetActiveCollisionOther(true);
            collision.AddForce(direction, 100f);
        }

        public IHolder Release()
        {
            transform.SetParent(null);
            OnHoldEvent?.Invoke(false);

            collision.SetActiveRigidbody(true);

            IHolder prevHolder = currentHolder;
            currentHolder = null;

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
