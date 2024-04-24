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

        private Transform visual;
        public Transform Visual => visual;

        private FSMBrain stateMachine;
        public FSMBrain StateMachine => stateMachine;

        public override void OnNetworkSpawn()
        {
            InputManager.ChangeInputMap(InputMapType.Play);//test

            stateMachine = GetComponent<FSMBrain>();
            visual = transform.Find("Visual");
            anim = visual.GetComponent<Animator>();

            stateMachine.Init();
        }

        public void Init(ulong ownerId)
        {
            NetworkObject.ChangeOwnership(ownerId);
        }

        private void Update()
        {
            stateMachine.UpdateFSM();
        }
    }
}
