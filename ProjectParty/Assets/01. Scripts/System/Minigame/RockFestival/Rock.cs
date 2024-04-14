using System;
using OMG.Interacting;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.RockFestival
{
    public class Rock : NetworkBehaviour, IHoldable, IFocusable
    {
        [SerializeField] UnityEvent onThrowEvent = null;
        [SerializeField] UnityEvent onSpawnedEvent = null;

        private IHolder currentHolder = null;
        public IHolder CurrentHolder => currentHolder;

        public GameObject CurrentObject => gameObject;

        private RockOwner rockOwner = null;
        private RockTransform rockTransform = null;
        private RockCollision rockCollision = null;

        public event Action<bool> OnHoldEvent = null;

        protected virtual void Awake()
        {
            rockOwner = GetComponent<RockOwner>();
            rockCollision = GetComponent<RockCollision>();
            rockTransform = GetComponent<RockTransform>();
        }

        public override void OnNetworkSpawn()
        {
            onSpawnedEvent?.Invoke();
        }

        /// <summary>
        /// Only Host Could Call this Method
        /// </summary>
        public virtual void Init()
        {
            rockCollision.Init();
        }

        public bool Hold(IHolder holder, Vector3 point = default)
        {
            if(holder.IsEmpty == false)
                return false;

            if(currentHolder != null)
                return false;

            currentHolder = holder;
            NetworkObject holderObject = currentHolder.HolderObject.GetComponent<NetworkObject>();
            rockOwner.SetOwner(holderObject.OwnerClientId, () => {
                rockTransform.SetPositionImmediately(currentHolder.HoldingParent.position);
                rockTransform.SetParent(currentHolder.HoldingParent);
            });

            rockCollision.SetActiveRigidbody(false);
            OnHoldEvent?.Invoke(true);

            return true;
        }

        public void Active()
        {
            Vector3 direction = currentHolder.HoldingParent.forward + Vector3.up * 0.5f;
            rockCollision.SetActiveCollisionOther(true);
            rockCollision.AddForce(direction, 12.5f);

            currentHolder.Release();
            onThrowEvent?.Invoke();
        }

        public IHolder Release()
        {
            IHolder prevHolder = currentHolder;
            currentHolder = null;
            
            if(rockCollision.ActiveCollisionOther == false)
                rockTransform.FitToGround();

            rockTransform.SetParent(null);
            rockCollision.SetActiveRigidbody(true);
            
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
