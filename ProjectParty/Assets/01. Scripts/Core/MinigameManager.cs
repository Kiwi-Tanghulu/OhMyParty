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

        private NetworkEvent onMinigameInitEvent = new NetworkEvent("MinigameInit");
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

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            onMinigameInitEvent.AddListener(HandleMinigameInit);
            onMinigameInitEvent.Register(NetworkObject);
        }

        public void StartMinigame(MinigameSO minigameData, params ulong[] joinedPlayers)
        {
            Debug.Log("start game");

            CurrentMinigame = Instantiate(minigameData.MinigamePrefab);
            CurrentMinigame.NetworkObject.Spawn(true);
            CurrentMinigame.SetPlayerDatas(joinedPlayers);
            onMinigameInitEvent?.Broadcast();
        }

        public void FinishMinigame()
        {
            Debug.Log("finish game");

            CurrentMinigame.Release();

            CurrentMinigame.MinigameData.OnMinigameFinishedEvent?.Invoke(CurrentMinigame);
            CurrentMinigame.NetworkObject.Despawn(true);
            CurrentMinigame = null;
        }

        private void HandleMinigameInit(NoneParams noneParams)
        {
            CurrentMinigame.Init();
        }
    }
}
