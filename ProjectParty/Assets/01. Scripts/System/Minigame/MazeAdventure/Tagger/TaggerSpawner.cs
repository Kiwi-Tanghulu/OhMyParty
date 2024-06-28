using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class TaggerSpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> taggerSpawnTrms;
        [SerializeField] private float taggerSpawnTime;
        [SerializeField] private GameObject taggerObj;
        [SerializeField] private int maxSpawnCount;

        private DeathmatchCycle cycle = null;
        private List<NetworkObject> taggerList = null;

        private void Awake()
        {
            taggerList = new List<NetworkObject>();
            cycle = GetComponent<DeathmatchCycle>();
            
        }
        private void SpawnTagger()
        {
            GameObject obj = Instantiate(taggerObj, taggerSpawnTrms[UnityEngine.Random.Range(0,taggerSpawnTrms.Count)].position, Quaternion.identity);
            Tagger tagger = obj.GetComponent<Tagger>();
            tagger.Init(cycle);
            tagger.NetworkObject.SpawnWithOwnership(0, true);
            tagger.NetworkObject.TrySetParent(gameObject, false);
            taggerList.Add(tagger.NetworkObject);
        }

        public void StartSpawn()
        {
            StartCoroutine(SpawnCycle());
        }
        public void StopSpawn()
        {
            StopAllCoroutines();
        }
        public void ClearTaggerList()
        {
            for(int i = 0; i < taggerList.Count; i++) 
            {
                if (taggerList[i] == null) continue;
                MinigameManager.Instance.CurrentMinigame.DespawnMinigameObject(taggerList[i], true);
            }
        }

        private IEnumerator SpawnCycle()
        {
            float time = taggerSpawnTime;
            while (true)
            {
                time += Time.deltaTime;
                if(time >= taggerSpawnTime)
                {
                    time = 0;
                    SpawnTagger();
                }
                yield return null;
            }
        }
    }

}
