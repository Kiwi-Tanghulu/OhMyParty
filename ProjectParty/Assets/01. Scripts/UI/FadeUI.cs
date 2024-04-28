using OMG.Utility;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace OMG.UI
{
    public class FadeUI : MonoBehaviour
    {
        [SerializeField] private OptOption<TimelineAsset> timelineOption = null;

        private PlayableDirector timelineHolder = null;

        private readonly int fadeInHash = Animator.StringToHash("fadeIn");
        private readonly int fadeOutHash = Animator.StringToHash("fadeOut");

        private void Awake()
        {
            timelineHolder = GetComponent<PlayableDirector>();
        }

        public void FadeIn()
        {
            Play(true);
        }

        public void FadeOut()
        {
            Play(false);
        }

        private void Play(bool option)
        {
            TimelineAsset timelineAsset = option ? timelineOption.PositiveOption : timelineOption.NegativeOption;
            timelineHolder.playableAsset = timelineAsset;

            timelineHolder.Play(timelineAsset);
        }
    }
}