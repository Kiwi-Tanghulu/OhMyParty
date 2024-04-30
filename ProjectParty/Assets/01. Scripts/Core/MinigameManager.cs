using OMG.UI;
using Unity.Netcode;
using UnityEngine;

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

            FadeUI.Instance.FadeIn(() =>
            {
                CurrentMinigame.Init(joinedPlayers);
                Time.timeScale = 0f;
            }, () =>
            {
                Time.timeScale = 1.0f;
            });
            
            // Test
            // CurrentMinigame.StartGame();
        }

        public void FinishMinigame()
        {
            FadeUI.Instance.FadeOut(null, () =>
            {
                CurrentMinigame.Release();

                CurrentMinigame.MinigameData.OnMinigameFinishedEvent?.Invoke(CurrentMinigame);
                CurrentMinigame.NetworkObject.Despawn(true);
                CurrentMinigame = null;

                FadeUI.Instance.FadeIn();
            });
        }
    }
}
