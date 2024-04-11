using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace OMG.Players
{
    public class LobbyActioningPlayer : ActioningPlayer
    {
        private PlayableDirector pd;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            pd = transform.Find("StartMinigameTimeline").GetComponent<PlayableDirector>();
            GameObject.FindObjectOfType<StartMinigameTimeline>().StartPlayerTimelineAction += pd.Play;
        }

        public override void OnNetworkDespawn()
        {
            GameObject.FindObjectOfType<StartMinigameTimeline>().StartPlayerTimelineAction -= pd.Play;
        }
    }
}
