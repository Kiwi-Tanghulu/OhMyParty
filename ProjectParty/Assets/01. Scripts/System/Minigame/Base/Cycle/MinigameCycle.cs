using OMG.Extensions;
using OMG.Lobbies;
using OMG.UI;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames
{
    public class MinigameCycle : NetworkBehaviour
    {
        protected Minigame minigame = null;
        protected MinigameCutscene cutscene = null;

        protected virtual void Awake()
        {
            cutscene = GetComponent<MinigameCutscene>();
            minigame = GetComponent<Minigame>();
        }

        public void PlayIntro()
        {
            cutscene.PlayCutsceneClientRpc(true);
        }

        public void PlayOutro()
        {
            cutscene.PlayCutsceneClientRpc(false);
        }

        public virtual void DisplayResult()
        {
            minigame.PlayerDatas.ForEach((minigameData, index) => {
                int score = minigame.CalculateScore(minigameData.score);
                Debug.Log($"[Minigame] Player {minigameData.clientID} Score : {score}");
                minigame.MinigamePanel.ResultPanel[index].SetResult($"Player {minigameData.clientID}", score);

                if(IsHost)
                {
                    Lobby.Current.PlayerDatas.ChangeData(j => j.ClientID == minigameData.clientID, lobbyData => {
                        lobbyData.Score += score;
                        return lobbyData;
                    });
                }
            });

            minigame.MinigamePanel.ResultPanel.Display(true);

            StartCoroutine(this.DelayCoroutine(minigame.MinigameData.ResultPostponeTime, () =>
            {
                Fade.Instance.FadeOut(0f, null, () =>
                {
                    if (IsHost)
                        MinigameManager.Instance.FinishMinigame();
                });
            }));
        }
    }
}
