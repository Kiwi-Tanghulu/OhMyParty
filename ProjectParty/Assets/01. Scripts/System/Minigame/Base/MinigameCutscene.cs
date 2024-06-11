using System.Collections.Generic;
using Cinemachine;
using OMG.Editors;
using OMG.Player;
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
            timelineHolder.Play();
        }
    }
}
