using OMG.FSM;
using OMG.Lobbies;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG
{
    public class CharacterController : NetworkBehaviour
    {
        private CharacterStat stat;
        public CharacterStat Stat => stat;

        private CharacterMovement movement;
        public CharacterMovement Movement => movement;

        private CharacterFSM fsm;
        public CharacterFSM FSM=> fsm;

        private List<CharacterComponent> compoList;

        public UnityEvent<ulong/*owner id*/> OnInitEvent;
        protected bool isInit = false;
#if UNITY_EDITOR
        [SerializeField] private bool useInNetwork = true;
#endif

        protected virtual void Awake()
        {
#if UNITY_EDITOR
            if (!useInNetwork)
                Init();
#endif
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            Init();
        }

        // call when spawned
        protected virtual bool Init()
        {
            if (isInit)
                return false;

            isInit = true;

            compoList = new List<CharacterComponent>();

            InitCompos();

            OnInitEvent?.Invoke(OwnerClientId);

            return true;
        }

        protected virtual void Update()
        {
            if (!isInit)
                return;

            UpdateController();
        }

        protected virtual void UpdateController()
        {
            for (int i = 0; i < compoList.Count; i++)
                compoList[i].UpdateCompo();
        }

        protected T InitCompo<T>(T compo) where T : CharacterComponent
        {
            if(compo == null)
            {
                Debug.LogError($"{typeof(T)} is null");

                return null;
            }

            compo.Init(this);
            compoList.Add(compo);

            return compo;
        }

        protected virtual void InitCompos()
        {
            stat = InitCompo(GetComponent<CharacterStat>());
            movement = InitCompo(GetComponent<CharacterMovement>());
            fsm = InitCompo(GetComponent<CharacterFSM>());
        }
    }
}