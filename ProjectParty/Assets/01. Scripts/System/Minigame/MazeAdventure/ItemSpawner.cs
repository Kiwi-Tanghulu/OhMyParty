using System.Collections;
using System.Collections.Generic;
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

        public void StartSpawn()
        {
            StartCoroutine(SpawnCycle());
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
