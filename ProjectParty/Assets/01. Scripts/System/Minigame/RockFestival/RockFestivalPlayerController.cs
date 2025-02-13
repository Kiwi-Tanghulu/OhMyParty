using OMG.Inputs;
using OMG.Interacting;
using OMG.Items;
using OMG.Player;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class RockFestivalPlayerController : PlayerController
    {
        [SerializeField] PlayInputSO input = null;

        private IHolder holder = null;
        private PlayerFocuser focuser = null;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if(IsOwner == false)
                return;

            holder = GetComponent<IHolder>();
            focuser = GetComponent<PlayerFocuser>();

            input.OnInteractEvent += HandleInteract;
        }

        protected override void Update()
        {
            base.Update();

            if(holder == null || holder.IsEmpty)
                return;
            
            Rock rock = holder.HoldingObject as Rock;
            if(rock == null)
                return;
            
            if(rock.HolderID != OwnerClientId)
                holder.Release();
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            if(IsOwner == false)
                return;

            input.OnInteractEvent -= HandleInteract;
        }

        private void HandleInteract(bool interact)
        {
            if (interact == false)
                return;

            if(holder.IsEmpty)
            {
                if(focuser.IsEmpty)
                    return;

                if(focuser.FocusedObject.CurrentObject.TryGetComponent<IHoldable>(out IHoldable target))
                    holder.Hold(target, focuser.FocusedPoint);
            }
            else
            {
                holder.Release();
            }
        }
    }
}
