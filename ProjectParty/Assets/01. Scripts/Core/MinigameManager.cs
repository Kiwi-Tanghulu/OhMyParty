using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames
{
    public class MinigameManager : NetworkBehaviour
    {
        public static MinigameManager Instance = null;

        public Minigame CurrentMinigame = null;

        private bool minigamePaused = false;
        public bool MinigamePaused { 
            get => minigamePaused;
            set {
                minigamePaused = value;
                Time.timeScale = minigamePaused ? 0 : 1;
            }
        }

        public void StartMinigame(MinigameSO minigameData, params ulong[] joinedPlayers)
        {
            Debug.Log("start game");

            CurrentMinigame = Instantiate(minigameData.MinigamePrefab);
            CurrentMinigame.NetworkObject.Spawn(true);
            CurrentMinigame.Init(joinedPlayers);
        }

        public void FinishMinigame()
        {
            Debug.Log("finish game");

            CurrentMinigame.Release();

            CurrentMinigame.MinigameData.OnMinigameFinishedEvent?.Invoke(CurrentMinigame);
            CurrentMinigame.NetworkObject.Despawn(true);
            CurrentMinigame = null;
        }
    }
}
