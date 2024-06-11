using OMG.Extensions;
using OMG.Lobbies;
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
                minigame.MinigameUI.ResultPanel[index].SetResult($"Player {minigameData.clientID}", score);

                if(IsHost)
                {
                    Lobby.Current.PlayerDatas.ChangeData(j => j.clientID == minigameData.clientID, lobbyData => {
                        lobbyData.score += score;
                        return lobbyData;
                    });
                }
            });

            minigame.MinigameUI.ResultPanel.Display(true);

            if(IsHost)
                StartCoroutine(this.DelayCoroutine(minigame.MinigameData.ResultPostponeTime, MinigameManager.Instance.FinishMinigame));
        }
    }
}
