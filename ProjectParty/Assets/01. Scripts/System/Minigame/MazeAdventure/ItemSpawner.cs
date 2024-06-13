using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace OMG.Minigames.MazeAdventure
{
    public class ItemSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject ItemBoxPrefab;
        [SerializeField] private float itemSpawnTime;
        [SerializeField] private Vector3 mapCenterPos;
        [SerializeField] private float range;

        private List<NetworkObject> currentItemList = null;
        public void StartSpawn()
        {
            currentItemList = new List<NetworkObject>();
            StartCoroutine(SpawnCycle());
        }
        public void StopSpawn()
        {
            StopAllCoroutines();
        }

        private void SpawnItem()
        {
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
            foreach (var itemBox in currentItemList)
            {
                MinigameManager.Instance.CurrentMinigame.DespawnMinigameObject(itemBox, true);
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
