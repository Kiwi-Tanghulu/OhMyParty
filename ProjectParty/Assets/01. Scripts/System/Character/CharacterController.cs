using System.Collections;
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

        private List<CharacterComponent> compoList;

        public UnityEvent<ulong/*owner id*/> OnSpawnedEvent;

        protected virtual void Awake()
        {
            compoList = new List<CharacterComponent>();

            stat = InitCompo(GetComponent<CharacterStat>());
            movement = InitCompo(GetComponent<CharacterMovement>());

            OnSpawnedEvent?.Invoke(OwnerClientId);
        }

        protected virtual void Update()
        {
            for (int i = 0; i < compoList.Count; i++)
                compoList[i].UpdateCompo();
        }

        protected T InitCompo<T>(T compo) where T : CharacterComponent
        {
            compo.Init(this);
            compoList.Add(compo);

            return compo;
        }
    }
}