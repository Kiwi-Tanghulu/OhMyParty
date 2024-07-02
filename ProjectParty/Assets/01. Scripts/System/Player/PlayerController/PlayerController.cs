using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using OMG.FSM;
using OMG.Inputs;
using OMG.Player.FSM;
using UnityEngine.Events;
using OMG.Extensions;

namespace OMG.Player
{
    public class PlayerController : CharacterController
    {
        private PlayerVisual visual;
        public PlayerVisual Visual => visual;

        private ExtendedAnimator animator;
        public ExtendedAnimator Animator => animator;

        private FSMBrain stateMachine;
        public FSMBrain StateMachine => stateMachine;

        protected override void Awake()
        {
            visual = transform.Find("Visual").GetComponent<PlayerVisual>();
            animator = visual.GetComponent<ExtendedAnimator>();

            base.Awake();
            
            if(stateMachine == null)
            {
                stateMachine = GetComponent<FSMBrain>();
                stateMachine.Init();
            }

            Debug.Log("awake");
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if(stateMachine == null)
            {
                stateMachine = GetComponent<FSMBrain>();
                stateMachine.Init();
                stateMachine.NetworkInit();
            }

            Debug.Log("spawn");
        }

        protected override void Update()
        {
            base.Update();

            stateMachine.UpdateFSM();
        }
    }
}