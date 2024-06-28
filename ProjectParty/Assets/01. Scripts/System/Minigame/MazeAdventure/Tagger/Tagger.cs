using OMG.FSM;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace OMG.Minigames.MazeAdventure
{
    public class Tagger : NetworkBehaviour
    {
        [SerializeField] private UnityEvent onSpawnedEvent = null;
        private FSMBrain brain = null;
        private NavMeshAgent navMeshAgent = null;
        public DeathmatchCycle Cycle { get; private set; }
        private void Awake()
        {
            brain = GetComponent<FSMBrain>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        public void Init(DeathmatchCycle cycle)
        {
            Cycle = cycle;
        }
        private void Update()
        {
            brain.UpdateFSM();
        }
        public override void OnNetworkSpawn()
        {
            onSpawnedEvent?.Invoke();
            if (IsHost)
            {
                brain.enabled = true;
                navMeshAgent.enabled = true;
                brain.Init();
            }
        }
    }
}
