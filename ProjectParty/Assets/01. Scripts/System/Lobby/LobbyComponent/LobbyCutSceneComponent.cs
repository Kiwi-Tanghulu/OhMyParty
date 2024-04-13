using OMG.Lobbies;
using OMG.Players;
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
    private PlayableDirector timelineHolder;

    private void Awake()
    {
        timelineHolder = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        Lobby.Current.GetLobbyComponent<LobbyReadyComponent>().OnLobbyReadyEvent += LobbyReadyComponent_OnLobbyReadyEvent;
    }

    private void LobbyReadyComponent_OnLobbyReadyEvent()
    {
        PlayStartGame();
    }

    public void PlayStartGame()
    {
        PlayCutsceneClientRpc(true);
    }

    public void PlayFinishGame()
    {
        PlayCutsceneClientRpc(false);
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
            }
        }

        timelineHolder.Play();
    }
}
