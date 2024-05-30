using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class ItemWall : MonoBehaviour
    {
        [SerializeField] private MeshCollider meshCollider;
        [SerializeField] private float keepTime;
        private MazeAdventureMapManager mapManager;

        public void StartCycle()
        {
            MazeAdventure mazeAdventure = MinigameManager.Instance.CurrentMinigame as MazeAdventure;
            mapManager = mazeAdventure.MapManager;
            StartCoroutine(ItemWallCycle());
        }
        private IEnumerator ItemWallCycle()
        {
            mapManager.BakeMap();
            yield return new WaitForSeconds(keepTime);
            meshCollider.enabled = false;
            mapManager.BakeMap();
            Destroy(gameObject);
        }
    }
}
