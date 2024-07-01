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

        public override void Awake()
        {
            visual = transform.Find("Visual").GetComponent<PlayerVisual>();
            animator = visual.GetComponent<ExtendedAnimator>();

            base.Awake();
            
            stateMachine = GetComponent<FSMBrain>();
            stateMachine.Init();
        }

        protected override void Update()
        {
            base.Update();

            stateMachine.UpdateFSM();
        }
    }
}