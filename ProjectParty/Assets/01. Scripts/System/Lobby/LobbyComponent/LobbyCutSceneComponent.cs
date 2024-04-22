using OMG.Lobbies;
using OMG.Player;
using OMG.Utility;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LobbyCutSceneComponent : LobbyComponent
{
    [SerializeField] private OptOption<TimelineAsset> timelineOption;
    [SerializeField] private string playerTrmTrackName;
    [SerializeField] private string playerAnimTrackName;
    [SerializeField] private string playerSignalTrackName;
    private PlayableDirector timelineHolder;

    private void Awake()
    {
        timelineHolder = GetComponent<PlayableDirector>();
    }

    public void PlayCutscene(bool option)
    {
        PlayCutsceneClientRpc(option);
    }

    [ClientRpc]
    private void PlayCutsceneClientRpc(bool option)
    {
        timelineHolder.playableAsset = timelineOption.GetOption(option);

        PlayerController[] players = Lobby.Current.PlayerContainer.PlayerList.ToArray();

        if (players == null)
            return;

        foreach (PlayableBinding binding in timelineHolder.playableAsset.outputs)
        {
            if(binding.streamName.Contains("Player"))
            {
                int index = int.Parse(binding.streamName.Last().ToString());

                if (players[index] == null)
                    continue;

                if (binding.streamName.Contains(playerTrmTrackName))
                    timelineHolder.SetGenericBinding(binding.sourceObject, players[index].GetComponent<Animator>());

                if (binding.streamName.Contains(playerAnimTrackName))
                    timelineHolder.SetGenericBinding(binding.sourceObject, players[index].Anim);

                if (binding.streamName.Contains(playerSignalTrackName))
                    timelineHolder.SetGenericBinding(binding.sourceObject, players[index].GetComponent<SignalReceiver>());
            }
        }

        timelineHolder.Play();
    }
}
