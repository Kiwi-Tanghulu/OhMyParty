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
        private Animator anim;
        public Animator Anim => anim;

        private PlayerVisual visual;
        public PlayerVisual Visual => visual;

        private FSMBrain stateMachine;
        public FSMBrain StateMachine => stateMachine;

        public override void OnNetworkSpawn()
        {
            InputManager.ChangeInputMap(InputMapType.Play);//test

            stateMachine = GetComponent<FSMBrain>();
            visual = transform.Find("Visual").GetComponent<PlayerVisual>();
            anim = visual.GetComponent<Animator>();

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
