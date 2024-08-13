using OMG.NetworkEvents;
using Unity.Netcode;
using UnityEngine;
using NetworkEvent = OMG.NetworkEvents.NetworkEvent;

namespace OMG.Minigames
{
    public class MinigameManager : NetworkBehaviour
    {
        private static MinigameManager instance = null;
        public static MinigameManager Instance => instance;

        public Minigame CurrentMinigame = null;

        private bool minigamePaused = false;
        public bool MinigamePaused { 
            get => minigamePaused;
            set {
                minigamePaused = value;
                Time.timeScale = minigamePaused ? 0 : 1;
            }
        }

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void StartMinigame(MinigameSO minigameData, ulong[] joinedPlayers)
        {
            Debug.Log("start game");

            CurrentMinigame = Instantiate(minigameData.MinigamePrefab);
            CurrentMinigame.NetworkObject.Spawn(true);
            CurrentMinigame.SetPlayerDatas(joinedPlayers);
        }

        public void FinishMinigame()
        {
            Debug.Log("finish game");

            CurrentMinigame.Release();
        }

        public void ReleaseMinigame()
        {
            CurrentMinigame.MinigameData.OnMinigameFinishedEvent?.Invoke(CurrentMinigame);
            CurrentMinigame.NetworkObject.Despawn(true);
            CurrentMinigame = null;
        }
    }
}
