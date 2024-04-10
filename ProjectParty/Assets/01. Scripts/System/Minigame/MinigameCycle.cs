using OMG.Extensions;
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
            // Set Result
            minigame.JoinedPlayers.ForEach(i => {
                Debug.Log($"[Minigame] Player {i.clientID} Score : {i.score}");
            });

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
