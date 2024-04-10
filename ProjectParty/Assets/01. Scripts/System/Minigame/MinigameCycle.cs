using OMG.Extensions;
using OMG.Lobbies;
using OMG.UI.Minigames;
using OMG.Utility;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace OMG.Minigames
{
    public class MinigameCycle : NetworkBehaviour
    {
        [SerializeField] OptOption<TimelineAsset> timelineOption = null;
        private PlayableDirector timelineHolder = null;
        
        protected Minigame minigame = null;

        protected virtual void Awake()
        {
            timelineHolder = GetComponent<PlayableDirector>();
            minigame = GetComponent<Minigame>();
        }

        public void PlayIntro()
        {
            PlayCutsceneClientRpc(true);
        }

        public void PlayOutro()
        {
            PlayCutsceneClientRpc(false);
        }

        public virtual void DisplayResult()
        {
            minigame.JoinedPlayers.ForEach((minigameData, index) => {
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

        [ClientRpc]
        private void PlayCutsceneClientRpc(bool option)
        {
            timelineHolder.playableAsset = timelineOption.GetOption(option);
            timelineHolder.Play();
        }
    }
}
