using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class TaggerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform testSpawnTrm;
        [SerializeField] private GameObject testObject;
        [SerializeField] private Transform taggerFindTrm;
        private void Start()
        {
            Debug.Log("SpawnTagger");
            GameObject obj = Instantiate(testObject, testSpawnTrm.position, Quaternion.identity);
            Tagger tagger = obj.GetComponent<Tagger>();
            tagger.NetworkObject.Spawn(true);
            tagger.NetworkObject.TrySetParent(gameObject, false);

            
        }
    }

}
