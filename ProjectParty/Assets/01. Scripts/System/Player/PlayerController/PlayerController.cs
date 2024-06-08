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
    public class PlayerController : NetworkBehaviour
    {
        [SerializeField] PlayInputSO input = null;

        private ExtendedAnimator animator;
        public ExtendedAnimator Animator => animator;

        private PlayerVisual visual;
        public PlayerVisual Visual => visual;

        private FSMBrain stateMachine;
        public FSMBrain StateMachine => stateMachine;

        private CharacterStat stat;
        public CharacterStat Stat => stat;

        public UnityEvent<ulong/*owner id*/> OnSpawnedEvent;

        public override void OnNetworkSpawn()
        {
            stateMachine = GetComponent<FSMBrain>();
            visual = transform.Find("Visual").GetComponent<PlayerVisual>();
            animator = visual.GetComponent<ExtendedAnimator>();
            stat = GetComponent<CharacterStat>();   

            stateMachine.Init();

            OnSpawnedEvent?.Invoke(OwnerClientId);
        }

        protected virtual void Update()
        {
            stateMachine.UpdateFSM();
        }

        private Coroutine inversionCoroutine = null;
        public void InvertInput(float duration)
        {
            input.MoveInputInversion = true;

            if(inversionCoroutine != null)
                StopCoroutine(inversionCoroutine);
            inversionCoroutine = StartCoroutine(this.DelayCoroutine(duration, () => input.MoveInputInversion = true));
        }
    }
}