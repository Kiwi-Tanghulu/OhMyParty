using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class TaggerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform testSpawnTrm;
        [SerializeField] private GameObject taggerObj;

        private DeathmatchCycle cycle = null;

        private void Awake()
        {
            cycle = GetComponent<DeathmatchCycle>();
        }
        public void TestSpawn()
        {
            GameObject obj = Instantiate(taggerObj, testSpawnTrm.position, Quaternion.identity);
            Tagger tagger = obj.GetComponent<Tagger>();
            tagger.Init(cycle);
            tagger.NetworkObject.Spawn(true);
            tagger.NetworkObject.TrySetParent(gameObject, false);
        }
    }

}
