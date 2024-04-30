using OMG.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace OMG.UI
{
    public enum FadeStateType
    {
        StartFadeIn = 0,
        EndFadeIn,
        StartFadeOut,
        EndFadeOut,
    }

    public class FadeUI : MonoBehaviour
    {
        public static FadeUI Instance { get; private set; }

        [SerializeField] private Transform fadeCamTrm;
        [SerializeField] private OptOption<TimelineAsset> timelineOption = null;

        public Dictionary<FadeStateType, Action> FadingEvents;

        private PlayableDirector timelineHolder = null;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            timelineHolder = GetComponent<PlayableDirector>();

            FadingEvents = new Dictionary<FadeStateType, Action>();
            foreach(FadeStateType type in Enum.GetValues(typeof(FadeStateType)))
                FadingEvents[type] = null;
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
            Transform mainCamTrm = Camera.main.transform;
            fadeCamTrm.SetPositionAndRotation(mainCamTrm.position, mainCamTrm.rotation);

            TimelineAsset timelineAsset = option ? timelineOption.PositiveOption : timelineOption.NegativeOption;
            timelineHolder.playableAsset = timelineAsset;

            timelineHolder.Play(timelineAsset);
        }

        public void InvokeFadingEvent(int type)
        {
            FadingEvents[(FadeStateType)type]?.Invoke();
        }
    }                      
}