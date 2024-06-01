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

    public class Fade : NetworkBehaviour
    {
        public static Fade Instance { get; private set; }

        [SerializeField] private Transform fadeCamTrm;

        [Space]
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
   
        public void FadeIn(float delay = 0f)
        {
            StartCoroutine(Play(delay, true, null, null));
        }

        public void FadeOut(float delay = 0f)
        {
            StartCoroutine(Play(delay, false, null, null));
        }

        public void FadeIn(float delay = 0f, Action onStartEvent = null, Action onEndEvent = null)
        {
            StartCoroutine(Play(delay, true, onStartEvent, onEndEvent));
        }

        public void FadeOut(float delay = 0f, Action onStartEvent = null, Action onEndEvent = null)
        {
            StartCoroutine(Play(delay, false, onStartEvent, onEndEvent));
        }

        private System.Collections.IEnumerator Play(float delay, bool option, Action onStartEvent, Action onEndEvents)
        {
            Time.timeScale = 0.0f;

            yield return new WaitForSecondsRealtime(delay);

            Time.timeScale = 1.0f;

            yield return null;


            if (!IsOwnedByServer)
                yield break;
            
            FadingEvents[FadeStateType.Begin] = onStartEvent;
            FadingEvents[FadeStateType.Finish] = onEndEvents;

            PlayClientRpc(option);
        }

        [ClientRpc]
        private void PlayClientRpc(bool option)
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