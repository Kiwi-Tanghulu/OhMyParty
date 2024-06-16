using OMG.Audio;
using System.Collections;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class ItemWall : MonoBehaviour
    {
        [SerializeField] private MeshCollider meshCollider;
        [SerializeField] private float keepTime;
        [SerializeField] private ParticleSystem itemWallEffect;
        private MazeAdventureMapManager mapManager;

        private AudioPlayer audioPlayer;
        private GameObject visual;

        private void Awake()
        {
            audioPlayer = GetComponent<AudioPlayer>();
            visual = transform.Find("Visual").gameObject;
        }

        public void StartCycle()
        {
            MazeAdventure mazeAdventure = MinigameManager.Instance.CurrentMinigame as MazeAdventure;
            mapManager = mazeAdventure.MapManager;
            StartCoroutine(ItemWallCycle());
        }
        private IEnumerator ItemWallCycle()
        {
            mapManager.BakeMap();
            audioPlayer.PlayOneShot("Maze_WallCreate");
            float effectRotationY = transform.rotation.eulerAngles.y + 90;
            ParticleSystem makeEffect = Instantiate(itemWallEffect, transform.position - Vector3.down * -1.25f, Quaternion.Euler(0f, effectRotationY, 0));
            makeEffect.transform.SetParent(MinigameManager.Instance.CurrentMinigame.transform);
            makeEffect.Play();
            yield return new WaitForSeconds(keepTime);
            ParticleSystem destroyEffect = Instantiate(itemWallEffect, transform.position - Vector3.down * -1.25f, Quaternion.Euler(0f, effectRotationY, 0));
            destroyEffect.transform.SetParent(MinigameManager.Instance.CurrentMinigame.transform);
            destroyEffect.Play(); 
            meshCollider.enabled = false;
            mapManager.BakeMap();
            audioPlayer.PlayOneShot("Maze_WallDestroy");
            visual.SetActive(false);

            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
        }
    }
}
