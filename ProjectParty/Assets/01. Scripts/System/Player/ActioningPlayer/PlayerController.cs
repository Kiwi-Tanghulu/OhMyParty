using OMG.Input;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Players
{
    public class PlayerController : NetworkBehaviour
    {
        private List<FSMState> states;
        [SerializeField] private FSMState startState;
        [SerializeField] private FSMState currentState;

        private bool isFirstEnabled = true;

        public override void OnNetworkSpawn()
        {
            InputManager.ChangeInputMap(InputMapType.Play);//test

            states = new List<FSMState>();
            Transform stateContainer = transform.Find("States");
            foreach (Transform stateTrm in stateContainer)
            {
                if (stateTrm.TryGetComponent<FSMState>(out FSMState state))
                {
                    states.Add(state);
                    state.InitState(this);
                }
            }

            if (startState == null)
            {
                Debug.Log("not set start state");
            }
            else
            {
                if (IsOwner)
                    ChangeState(startState);
            }
        }

        public void Init(ulong ownerId)
        {
            NetworkObject.ChangeOwnership(ownerId);
        }

        private void OnEnable()
        {
            if (isFirstEnabled)
                isFirstEnabled = false;
            else
                currentState?.EnterState();
        }

        private void OnDisable()
        {
            currentState?.ExitState();
        }

        private void Update()
        {
            if (!IsSpawned)
                return;

            currentState?.UpdateState();
        }

        public void ChangeState(Type type)
        { 
            FSMState state = states.Find(x => x.GetType() == type);
            if (state != null)
                ChangeState(state);
        }

        public void ChangeState(FSMState state)
        {
            if (!states.Find(x => x == state))
            {
                Debug.Log($"not exist : {state}");
                return;
            }

            if (currentState == state)
                return;

            int index = states.IndexOf(state);

            ChangeStateServerRpc(index);
        }

        [ServerRpc(RequireOwnership = false)]
        private void ChangeStateServerRpc(int stateIndex)
        {
            ChangeStateClientRpc(stateIndex);
        }

        [ClientRpc]
        private void ChangeStateClientRpc(int stateIndex)
        {
            currentState?.ExitState();
            currentState = states[stateIndex];
            currentState.EnterState();
        }
    }
}
