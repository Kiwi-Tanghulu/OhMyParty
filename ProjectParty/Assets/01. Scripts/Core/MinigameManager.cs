using System;
using Unity.Netcode;

namespace OMG.Minigames
{
    public class MinigameManager : NetworkBehaviour
    {
        public static MinigameManager Instance = null;

        public Minigame CurrentMinigame { get; private set; } = null;

        public event Action OnMinigameFinishEvent = null;

        public void StartMinigame(Minigame minigame)
        {
            CurrentMinigame = Instantiate(minigame);
            CurrentMinigame.NetworkObject.Spawn(true);

            CurrentMinigame.StartGame();
        }

        public void FinishMinigame()
        {
            Destroy(CurrentMinigame.gameObject);
            CurrentMinigame = null;

            OnMinigameFinishEvent?.Invoke();
        }
    }
}
