using OMG.Extensions;
using OMG.Inputs;
using OMG.Lobbies;
using OMG.Player;
using OMG.UI;
using OMG.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace OMG.Lobbies
{
    public enum LobbyCutSceneState
    {
        StartBegin,
        StartFinish,
        EndBegin,
        EndFinish
    }

    public class LobbyCutSceneComponent : LobbyComponent
    {
        [SerializeField] private OptOption<TimelineAsset> timelineOption;
        [SerializeField] private string playerTrmTrackName;
        [SerializeField] private string playerAnimTrackName;
        [SerializeField] private string playerSignalTrackName;
        private PlayableDirector timelineHolder;

        public Dictionary<LobbyCutSceneState, Action> CutSceneEvents;

        public bool IsPlaying => timelineHolder.state == PlayState.Playing;

        private void Awake()
        {
            timelineHolder = GetComponent<PlayableDirector>();

            CutSceneEvents = new Dictionary<LobbyCutSceneState, Action>();
            foreach (LobbyCutSceneState type in Enum.GetValues(typeof(LobbyCutSceneState)))
                CutSceneEvents[type] = null;
        }

        public void PlayCutscene(bool option)
        {
            Debug.Log($"play cut scene option : {option}");

            //PlayCutsceneClientRpc(option);

            timelineHolder.playableAsset = timelineOption.GetOption(option);

            timelineHolder.Play();

            if (option)
                CutSceneEvents[LobbyCutSceneState.StartBegin]?.Invoke();
            else
                CutSceneEvents[LobbyCutSceneState.EndBegin]?.Invoke();

            //InputManager.SetInputEnable(false);

            StartCoroutine(DelayFinish(() =>
            {
                //InputManager.SetInputEnable(true);

                if (option)
                    CutSceneEvents[LobbyCutSceneState.StartFinish]?.Invoke();
                else
                    CutSceneEvents[LobbyCutSceneState.EndFinish]?.Invoke();
            }));
        }

        private System.Collections.IEnumerator DelayFinish( Action action)
        {
            yield return new WaitUntil(() => timelineHolder.state != PlayState.Playing);

            action?.Invoke();
        }
    }
}