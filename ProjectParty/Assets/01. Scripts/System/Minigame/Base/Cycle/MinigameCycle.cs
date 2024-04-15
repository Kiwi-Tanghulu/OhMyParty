using OMG.Extensions;
using OMG.Lobbies;
using OMG.Players;
using OMG.Utility;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace OMG.Minigames
{
    [RequireComponent(typeof(PlayableDirector))]
    [RequireComponent(typeof(SignalReceiver))]
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
            PlayCutsceneClientRpc(true, minigame is PlayableMinigame);
        }

        public void PlayOutro()
        {
            PlayCutsceneClientRpc(false, minigame is PlayableMinigame);
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

        [ClientRpc]
        private void PlayCutsceneClientRpc(bool option, bool bindPlayer)
        {
            timelineHolder.playableAsset = timelineOption.GetOption(option);
            
            bindPlayer &= minigame is PlayableMinigame;
            if(bindPlayer)
            {
                NetworkList<NetworkObjectReference> players = (minigame as PlayableMinigame).Players;
                int bindedCount = 0;

                foreach(PlayableBinding binding in timelineHolder.playableAsset.outputs)
                {
                    if(binding.streamName == "Player")
                    {
                        if(players[bindedCount].TryGet(out NetworkObject player))
                        {
                            PlayerController minigamePlayer = player.GetComponent<PlayerController>();
                            timelineHolder.SetGenericBinding(binding.sourceObject, minigamePlayer.Anim);

                            bindedCount++;
                            if(bindedCount >= minigame.PlayerDatas.Count)
                                break;
                        }
                    }
                }
            }

            timelineHolder.Play();
        }
    }
}
