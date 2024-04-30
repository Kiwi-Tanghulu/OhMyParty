using Unity.Netcode;

namespace OMG.Minigames
{
    public class MinigameManager : NetworkBehaviour
    {
        public static MinigameManager Instance = null;

        public Minigame CurrentMinigame = null;

        public void StartMinigame(MinigameSO minigameData, params ulong[] joinedPlayers)
        {
            CurrentMinigame = Instantiate(minigameData.MinigamePrefab);
            CurrentMinigame.NetworkObject.Spawn(true);
            CurrentMinigame.Init(joinedPlayers);

            // Test
            // CurrentMinigame.StartGame();
        }

        public void FinishMinigame()
        {
            CurrentMinigame.Release();

            CurrentMinigame.MinigameData.OnMinigameFinishedEvent?.Invoke(CurrentMinigame);
            CurrentMinigame.NetworkObject.Despawn(true);
            CurrentMinigame = null;
        }
    }
}
