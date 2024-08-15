using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace OMG.Minigames.MazeAdventure
{
    public class ItemSpawner : NetworkBehaviour
    {
        [SerializeField] private GameObject ItemBoxPrefab;
        [SerializeField] private float itemSpawnTime;
        [SerializeField] private Vector3 mapCenterPos;
        [SerializeField] private float range;

        private List<NetworkObject> currentItemList = null;
        private bool canSpawn;
        public void StartSpawn()
        {
            canSpawn = true;
            currentItemList = new List<NetworkObject>();
            StartCoroutine(SpawnCycle());
        }
        public void StopSpawn()
        {
            canSpawn = false;
            StopAllCoroutines();
        }

        private void SpawnItem()
        {
            if (!canSpawn) return;
            Vector3 spawnPos;
            Vector3 randomPoint = (transform.position + mapCenterPos) + Random.insideUnitSphere * range;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, range, NavMesh.AllAreas))
            {
                spawnPos = hit.position + Vector3.up * 1f;
            }
            else
            {
                spawnPos = Vector3.zero;
            }

            GameObject obj = Instantiate(ItemBoxPrefab, spawnPos, Quaternion.identity);
            ItemBox itemBox = obj.GetComponent<ItemBox>();
            itemBox.NetworkObject.SpawnWithOwnership(0, true);
            itemBox.NetworkObject.TrySetParent(gameObject, true);
            currentItemList.Add(itemBox.NetworkObject);
        }

        public void ClearItemBoxList()
        {
            if (currentItemList == null)
                return;

            for (int i = 0; i < currentItemList.Count; i++)
            {
                if (currentItemList[i] == null) continue;
                MinigameManager.Instance.CurrentMinigame.DespawnMinigameObject(currentItemList[i], true);
            }
        }

        private IEnumerator SpawnCycle()
        {
            while (true)
            {
                yield return new WaitForSeconds(itemSpawnTime);
                SpawnItem();
            }
        }
    }
}
