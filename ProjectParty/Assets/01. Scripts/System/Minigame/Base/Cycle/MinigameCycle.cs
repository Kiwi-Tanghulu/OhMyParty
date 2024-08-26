using OMG.Extensions;
using OMG.Inputs;
using OMG.Lobbies;
using OMG.UI;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames
{
    public class MinigameCycle : NetworkBehaviour
    {
        protected Minigame minigame = null;

        public virtual void Init(Minigame minigame)
        {
            this.minigame = minigame;
        }

        public virtual void DisplayResult()
        {
            minigame.PlayerDatas.ForEach((minigameData, index) => {
                int score = minigame.CalculateScore(minigameData.score);
                Lobby.Current.PlayerDatas.Find(out Lobbies.PlayerData data,
                    data => data.ClientID == minigameData.clientID);
                string name = data.Nickname;
                if(name == null)
                    name = $"Player {minigameData.clientID}";
            
                Debug.Log($"[Minigame] Player {minigameData.clientID} Score : {score}");
                minigame.MinigamePanel.ResultPanel[index].SetResult($"{name}", score);

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
                Fade.Instance.FadeOut(0f, () =>
                {
                    InputManager.SetInputEnable(false);
                    DEFINE.MinigameCanvas.ChangeRenderTypea(1f / minigame.transform.localScale.x);
                }, () =>
                {
                    if (IsHost)
                        minigame.Release();
                });
            }));
        }
    }
}
