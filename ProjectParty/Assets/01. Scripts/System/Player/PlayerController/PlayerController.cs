using OMG.FSM;
using OMG.Lobbies;
using UnityEngine;

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
        }

        protected override void Start()
        {
            base.Start();
#if UNITY_EDITOR
            if (Lobby.Current == null)
            {
                
                OnSpawnedEvent?.Invoke(OwnerClientId);
                stateMachine = GetComponent<FSMBrain>();
                stateMachine.Init();
            }
#endif
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            stateMachine = GetComponent<FSMBrain>();
            stateMachine.Init();
            stateMachine.NetworkInit();
        }

        protected override void Update()
        {
            base.Update();

            stateMachine.UpdateFSM();
        }
    }
}