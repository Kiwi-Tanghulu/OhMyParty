using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class TaggerSpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> taggerSpawnTrms;
        [SerializeField] private float taggerSpawnTime;
        [SerializeField] private GameObject taggerObj;

        private DeathmatchCycle cycle = null;

        private void Awake()
        {
            cycle = GetComponent<DeathmatchCycle>();
        }
        private void SpawnTagger()
        {
            GameObject obj = Instantiate(taggerObj, taggerSpawnTrms[Random.Range(0,taggerSpawnTrms.Count)].position, Quaternion.identity);
            Tagger tagger = obj.GetComponent<Tagger>();
            tagger.Init(cycle);
            tagger.NetworkObject.SpawnWithOwnership(0, true);
            tagger.NetworkObject.TrySetParent(gameObject, false);
        }
        public void StartSpawn()
        {
            StartCoroutine(SpawnCycle());
        }

        private IEnumerator SpawnCycle()
        {
            float time = taggerSpawnTime;
            while (true)
            {
                time += Time.deltaTime;
                if(time >= taggerSpawnTime)
                {
                    SpawnTagger();
                    time = 0;
                }
                yield return null;
            }
        }
    }

}
