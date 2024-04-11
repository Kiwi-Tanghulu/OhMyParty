using System;
using OMG.Input;
using OMG.Interacting;
using UnityEngine;

namespace OMG.Players
{
    public class PlayerHand : MonoBehaviour, IHolder
    {
        [SerializeField] PlayInputSO input = null;

        [SerializeField] Transform holdingParent = null;
        public Transform HoldingParent => holdingParent;

        private IHoldable holdingObject = null;
        public IHoldable HoldingObject => holdingObject;

        private PlayerFocuser focuser = null;

        public bool IsEmpty => holdingObject == null;

        private void Awake()
        {
            focuser = GetComponent<PlayerFocuser>();
            input.OnInteractEvent += HandleInteract;
            input.OnActiveEvent += HandleActive;
        }

        private void OnDestroy()
        {
            input.OnInteractEvent -= HandleInteract;
            input.OnActiveEvent -= HandleActive;
        }

        public bool Hold(IHoldable target, Vector3 point = default)
        {
            bool result = target.Hold(this, point);
            Debug.Log($"Hold Result : {result}");
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

        private void HandleInteract(bool interact)
        {
            if(IsEmpty)
            {
                if(interact == false)
                    return;

                if(focuser.IsEmpty)
                    return;

                if(focuser.FocusedObject.CurrentObject.TryGetComponent<IHoldable>(out IHoldable target))
                    Hold(target, focuser.FocusedPoint);
            }
            else
            {
                Release();
            }
        }

        private void HandleActive()
        {
            Active();
        }
    }
}
