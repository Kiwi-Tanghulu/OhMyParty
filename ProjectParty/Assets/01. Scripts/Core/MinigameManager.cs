using OMG.UI;
using System.Collections;
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
            CurrentMinigame.Init(joinedPlayers);

            Fade.Instance.FadeIn(3f, () =>
            {
                Time.timeScale = 0f;
            }, () =>
            {
                Time.timeScale = 1.0f;
            });
        }

        public void FinishMinigame()
        {
            Fade.Instance.FadeOut(0f, null, () =>
            {
                CurrentMinigame.Release();

                CurrentMinigame.MinigameData.OnMinigameFinishedEvent?.Invoke(CurrentMinigame);
                CurrentMinigame.NetworkObject.Despawn(true);
                CurrentMinigame = null;

                Fade.Instance.FadeIn(3f, () =>
                {
                    Time.timeScale = 0f;
                }, () =>
                {
                    Time.timeScale = 1.0f;
                });
            });
        }
    }
}
