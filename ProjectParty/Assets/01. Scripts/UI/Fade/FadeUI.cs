using OMG.Utility;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace OMG.UI
{
    public enum FadeStateType
    {
        Begin,
        Finish
    }

    public class FadeUI : NetworkBehaviour
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
            Play(true, null, null);
        }

        public void FadeOut()
        {
            Play(false, null, null);
        }

        public void FadeIn(Action onStartEvent = null, Action onEndEvent = null)
        {
            Play(true, onStartEvent, onEndEvent);
        }

        public void FadeOut(Action onStartEvent = null, Action onEndEvent = null)
        {
            Play(false, onStartEvent, onEndEvent);
        }

        private void Play(bool option, Action onStartEvent, Action onEndEvents)
        {
            if (!IsOwnedByServer)
                return;

            FadingEvents[FadeStateType.Begin] = onStartEvent;
            FadingEvents[FadeStateType.Finish] = onEndEvents;

            PlayClientRpc(option);
        }

        [ClientRpc]
        private void PlayClientRpc(bool option)
        {
            Debug.Log(1);
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