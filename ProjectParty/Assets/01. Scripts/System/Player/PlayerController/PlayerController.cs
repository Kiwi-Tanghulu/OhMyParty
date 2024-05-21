using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using OMG.FSM;
using OMG.Input;
using OMG.Player.FSM;

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

        protected virtual void Update()
        {
            stateMachine.UpdateFSM();

            if (UnityEngine.Input.GetKeyDown(KeyCode.X))
                stateMachine.ChangeState(typeof(StunState));
        }
    }
}
