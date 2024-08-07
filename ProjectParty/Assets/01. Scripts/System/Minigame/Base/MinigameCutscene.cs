using Cinemachine;
using OMG.UI;
using OMG.Utility;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace OMG.Minigames
{
    [RequireComponent(typeof(PlayableDirector))]
    [RequireComponent(typeof(SignalReceiver))]
    public class MinigameCutscene : NetworkBehaviour
    {
        [SerializeField] GameCycleText cycleText = null;
        [SerializeField] OptOption<TimelineAsset> timelineOption = null;
        protected PlayableDirector timelineHolder = null;
        protected Minigame minigame = null;

        protected virtual void Awake()
        {
            timelineHolder = GetComponent<PlayableDirector>();
            minigame = GetComponent<Minigame>();
        }

        [ClientRpc]
        public void PlayCutsceneClientRpc(bool option)
        {
            timelineHolder.playableAsset = timelineOption.GetOption(option);
            BindingTimeLineObject(timelineHolder, option);

            timelineHolder.Play();
        }

        protected virtual void BindingTimeLineObject(PlayableDirector timelineHolder, bool option)
        {
            foreach (PlayableBinding binding in timelineHolder.playableAsset.outputs)
            {
                if (binding.streamName == "Cinemachine Track")
                {
                    timelineHolder.SetGenericBinding(binding.sourceObject, Camera.main.GetComponent<CinemachineBrain>());
                }
            }
        }

        private void Update()
        {
            Debug.Log(timelineHolder.time);
        }

        public void SkipCutscene()
        {
            timelineHolder.time = timelineHolder.playableAsset.duration;
            timelineHolder.Evaluate();
            //timelineHolder.Stop();
            timelineHolder.Pause();
            cycleText.PlayRaedyGo();
        }
    }
}
