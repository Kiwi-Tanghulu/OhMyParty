using OMG.FSM;
using OMG.Lobbies;
using Steamworks;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerController : CharacterController
    {
        private PlayerVisual visual;
        public PlayerVisual Visual => visual;

        private ExtendedAnimator animator;
        public ExtendedAnimator Animator => animator;

        public override bool Init(ulong ownerID)
        {
            bool result = base.Init(ownerID);

            if(result)
            {
                visual = transform.Find("Visual").GetComponent<PlayerVisual>();
                animator = visual.GetComponent<ExtendedAnimator>();
            }

            return result;
        }

        //public override void OnNetworkSpawn()
        //{
        //    base.OnNetworkSpawn();
        //    stateMachine = GetComponent<FSMBrain>();
        //    stateMachine.Init();
        //    stateMachine.NetworkInit();
        //}
    }
}