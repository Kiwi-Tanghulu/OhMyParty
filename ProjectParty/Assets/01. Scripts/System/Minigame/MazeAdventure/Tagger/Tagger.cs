using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.MazeAdventure
{
    public class Tagger : NetworkBehaviour
    {
        [SerializeField] private UnityEvent onSpawnedEvent = null;
        
        [SerializeField] private float moveSpeed;
        private Vector3 moveDir;
        private float testTimer = 0;
        public override void OnNetworkSpawn()
        {
            onSpawnedEvent?.Invoke();
            moveDir = Vector3.left;
        }
        private void Update()
        {
            if (!IsHost) return;
            testTimer += Time.deltaTime;
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            if(testTimer > 3f)
            {
                moveDir *= -1;
                testTimer = 0;
            }
        }
    }
}
