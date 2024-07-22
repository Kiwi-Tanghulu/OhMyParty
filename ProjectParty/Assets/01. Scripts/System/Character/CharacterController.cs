using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG
{
    public class CharacterController : NetworkBehaviour
    {
        [SerializeField] private List<CharacterComponent> compoList;
        private Dictionary<Type, CharacterComponent> compoDictionary;

        [Space]
        public UnityEvent<ulong/*owner id*/> OnInitEvent;
        protected bool isInit = false;

#if UNITY_EDITOR
        [SerializeField] private bool useInNetwork = true;
        public bool UseInNetwork => useInNetwork;
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

        private void InitCompos()
        {
            compoDictionary = new Dictionary<Type, CharacterComponent>();

            foreach (CharacterComponent compo in compoList)
            {
                Type type = compo.GetType();
                while(true)
                {
                    compoDictionary.Add(type, compo);
                    type = type.BaseType;

                    if (type == typeof(CharacterComponent))
                        break;
                }
            }

            foreach (CharacterComponent compo in compoList)
            {
                compo.Init(this);
            }

            foreach (CharacterComponent compo in compoList)
            {
                compo.PostInitializeComponent();
            }
        }

        public T GetCharacterComponent<T>() where T : CharacterComponent
        {
            if (!compoDictionary.ContainsKey(typeof(T)))
            {
                Debug.LogError($"not exsist compo : {typeof(T)}");
                return null;
            }

            return compoDictionary[typeof(T)] as T;
        }
        
        public virtual void Respawn(Transform respawnTrm) { }
    }
}