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
        
        [SerializeField] private float moveSpeed;
        private Vector3 moveDir;
        private float testTimer = 0;
        private FSMBrain brain;
        private NavMeshAgent navMeshAgent;
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
