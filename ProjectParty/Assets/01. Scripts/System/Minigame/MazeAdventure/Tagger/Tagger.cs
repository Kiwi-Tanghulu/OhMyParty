using OMG.FSM;
using OMG.Player;
using Steamworks.Data;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace OMG.Minigames.MazeAdventure
{
    public class Tagger : CharacterController
    {
        [SerializeField] private UnityEvent onSpawnedEvent = null;
        private CharacterFSM brain = null;
        private NavMeshAgent navMeshAgent = null;
        public DeathmatchCycle Cycle { get; private set; }

        protected override bool Init()
        {
            bool result = base.Init();

            if (result)
            {
                navMeshAgent = GetComponent<NavMeshAgent>();

                onSpawnedEvent?.Invoke();

                if (IsHost)
                {
                    FSM.enabled = true;
                    navMeshAgent.enabled = true;
                }
            }

            return result;
        }

        public void SetCycle(DeathmatchCycle cycle)
        {
            Cycle = cycle;
        }
    }
}