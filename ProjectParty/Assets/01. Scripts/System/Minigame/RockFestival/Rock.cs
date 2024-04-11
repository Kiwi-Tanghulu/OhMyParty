using System;
using OMG.Interacting;
using OMG.Utility.Transforms;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class Rock : NetworkBehaviour, IHoldable, IFocusable
    {
        private IHolder currentHolder = null;
        public IHolder CurrentHolder => currentHolder;

        public GameObject CurrentObject => gameObject;

        private SubstituteParent parent = null;
        private RockCollision collision = null;

        public event Action<bool> OnHoldEvent = null;

        protected virtual void Awake()
        {
            collision = GetComponent<RockCollision>();
            parent = GetComponent<SubstituteParent>();
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
            parent.SetParent(currentHolder.HoldingParent);
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
            collision.AddForce(direction, 10f);
        }

        public IHolder Release()
        {
            IHolder prevHolder = currentHolder;
            currentHolder = null;
            
            parent.SetParent(null);

            if(collision.ActiveCollisionOther == false)
                collision.FitToGround();
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
