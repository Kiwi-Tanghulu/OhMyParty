using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using static UnityEngine.UI.Image;

namespace OMG.Items
{
    public class BananaSpawnItem : PlayerItem
    {
        [SerializeField] private GameObject bananaPrefab;
        [SerializeField] private float spawnDistance;
        [SerializeField] private LayerMask spawnGroundLayer;

        public override void OnActive()
        {
            Vector3 spawnPos = ownerPlayer.transform.position - (ownerPlayer.transform.forward * spawnDistance);
            
            if (Physics.Raycast(spawnPos + Vector3.up * 1000, Vector3.down,
                out RaycastHit hit, 10000, spawnGroundLayer))
            {
                spawnPos = hit.point;

                Instantiate(bananaPrefab, spawnPos, Quaternion.identity,
                    MinigameManager.Instance.CurrentMinigame.transform);
            }
        }
    }
}
