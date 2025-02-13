using Cinemachine;
using OMG.UI;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace OMG.Minigames
{
    [RequireComponent(typeof(PlayableDirector))]
    public class MinigameCutscene : NetworkBehaviour
    {
        [SerializeField] GameCycleText gameCycleTextUI = null;
        [SerializeField] TimelineAsset cutscene = null;

        [Space(15f)]
        [SerializeField] UnityEvent onCutsceneFinishEvent = null;

        protected PlayableDirector timelineHolder = null;
        protected Minigame minigame = null;
        protected bool cutsceneOption = false;

        protected virtual void Awake()
        {
            timelineHolder = GetComponent<PlayableDirector>();
            minigame = GetComponent<Minigame>();
        }

        public void PlayCutscene()
        {
            timelineHolder.playableAsset = cutscene;
            BindingTimeLineObject(timelineHolder);

            timelineHolder.Play();
            timelineHolder.stopped += HandleTimelineStopped;
        }

        protected virtual void BindingTimeLineObject(PlayableDirector timelineHolder)
        {
            foreach (PlayableBinding binding in timelineHolder.playableAsset.outputs)
            {
                if (binding.streamName == "Cinemachine Track")
                {
                    timelineHolder.SetGenericBinding(binding.sourceObject, Camera.main.GetComponent<CinemachineBrain>());
                }
            }
        }

        private void HandleTimelineStopped(PlayableDirector director)
        {
            director.stopped -= HandleTimelineStopped;

            gameCycleTextUI.ReadyGo.AddGoNegativeCallback(() => {
                onCutsceneFinishEvent?.Invoke();
                minigame.State.ChangeState(MinigameState.CutsceneFinished);
            });
            
            minigame.CutscenePanel.Display(false);
            gameCycleTextUI.ReadyGo.Play();
        }

        public void SkipCutscene()
        {
            timelineHolder.time = timelineHolder.playableAsset.duration;
            timelineHolder.Evaluate();
            timelineHolder.Stop();
            timelineHolder.Pause();
        }
    }
}
