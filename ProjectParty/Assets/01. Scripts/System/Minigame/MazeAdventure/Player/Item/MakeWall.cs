using OMG.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class MakeWall : MazeAdventureItem
    {
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private float makeLength;
        [SerializeField] private float makeHeight;
        public override void OnActive()
        {
            Vector3 makePos = playerTrm.position + playerTrm.forward * makeLength + playerTrm.up * makeHeight;
            Vector3 lookDirection = playerTrm.forward;
            Quaternion lookRotation = Quaternion.identity;
            lookDirection.y = 0; // y축 방향 제거
            if (lookDirection != Vector3.zero)
            {
                lookRotation  = Quaternion.LookRotation(lookDirection);
            }
            ItemWall wall = Instantiate(wallPrefab, makePos, lookRotation).GetComponent<ItemWall>();
            wall.transform.SetParent(MinigameManager.Instance.CurrentMinigame.transform);
            wall.StartCycle();
        }
    }
}
