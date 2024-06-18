using OMG.Items;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class MakeWallItemBehaviour : NetworkItemBehaviour
    {
        [SerializeField] private GameObject wallPrefab;

        [ServerRpc]
        public void MakeWallServerRPC(Vector3 makePos, Vector3 lookDiretion)
        {
            MakeWallClientRPC(makePos, lookDiretion);
        }

        [ClientRpc]
        public void MakeWallClientRPC(Vector3 makePos, Vector3 lookDirection)
        {
            Quaternion lookRotation = Quaternion.identity;
            lookDirection.y = 0; // y축 방향 제거
            if (lookDirection != Vector3.zero)
            {
                lookRotation = Quaternion.LookRotation(lookDirection);
            }
            ItemWall wall = Instantiate(wallPrefab, makePos, lookRotation).GetComponent<ItemWall>();
            wall.transform.SetParent(MinigameManager.Instance.CurrentMinigame.transform);
            wall.StartCycle();
        }
    }
}
