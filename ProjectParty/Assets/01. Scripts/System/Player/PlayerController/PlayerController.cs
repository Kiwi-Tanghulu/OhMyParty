using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using OMG.FSM;
using OMG.Input;

namespace OMG.Player
{
    public class PlayerController : NetworkBehaviour
    {
        private ExtendedAnimator animator;
        public ExtendedAnimator Animator => animator;

        private PlayerVisual visual;
        public PlayerVisual Visual => visual;

        private FSMBrain stateMachine;
        public FSMBrain StateMachine => stateMachine;

        public override void OnNetworkSpawn()
        {
            stateMachine = GetComponent<FSMBrain>();
            visual = transform.Find("Visual").GetComponent<PlayerVisual>();
            animator = visual.GetComponent<ExtendedAnimator>();

            stateMachine.Init();
        }

        public virtual void Init(ulong ownerId)
        {
            NetworkObject.ChangeOwnership(ownerId);
        }

        protected virtual void Update()
        {
            stateMachine.UpdateFSM();
        }
    }
}
