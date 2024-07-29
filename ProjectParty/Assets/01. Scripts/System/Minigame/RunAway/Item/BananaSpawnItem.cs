using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Items
{
    public class BananaSpawnItem : PlayerItem
    {
        [SerializeField] private GameObject bananaPrefab;
        [SerializeField] private float spawnDistance;

        public override void OnActive()
        {
            Vector3 spawnPos = ownerPlayer.transform.position - (ownerPlayer.transform.forward * spawnDistance);
            Instantiate(bananaPrefab, spawnPos, Quaternion.identity,
                MinigameManager.Instance.CurrentMinigame.transform);
        }
    }
}
