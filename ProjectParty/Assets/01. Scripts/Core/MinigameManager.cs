using Unity.Netcode;

namespace OMG.Minigames
{
    public class MinigameManager : NetworkBehaviour
    {
        public static MinigameManager Instance = null;

        public Minigame CurrentMinigame { get; private set; } = null;

        public void StartMinigame(Minigame minigame)
        {
            SceneManager.Instance.LoadScene(SceneType.MinigameScene, false, () => {
                CurrentMinigame = Instantiate(minigame);
                CurrentMinigame.NetworkObject.Spawn(true);

                CurrentMinigame.StartGame();
            });
        }

        public void FinishMinigame()
        {
            // Destroy(CurrentMinigame);
            CurrentMinigame = null;

            SceneManager.Instance.LoadScene(SceneType.LobbyScene, false, () => {
                // lobby setting
                // settled result
            });
        }
    }
}
