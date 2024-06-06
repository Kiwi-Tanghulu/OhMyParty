using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using OMG.FSM;
using OMG.Inputs;
using OMG.Player.FSM;
using UnityEngine.Events;

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

        public UnityEvent<ulong/*owner id*/> OnSpawnedEvent;

        public override void OnNetworkSpawn()
        {
            stateMachine = GetComponent<FSMBrain>();
            visual = transform.Find("Visual").GetComponent<PlayerVisual>();
            animator = visual.GetComponent<ExtendedAnimator>();

            stateMachine.Init();

            OnSpawnedEvent?.Invoke(OwnerClientId);
        }

        protected virtual void Update()
        {
            stateMachine.UpdateFSM();
        }
    }
}