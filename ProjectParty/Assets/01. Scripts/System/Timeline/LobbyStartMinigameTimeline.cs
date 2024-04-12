using OMG.Lobbies;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Playables;

public class LobbyStartMinigameTimeline : MonoBehaviour
{
    public event Action StartPlayerTimelineAction;

    private PlayableDirector pd;
    private LobbyReadyComponent ready;

    private void Awake()
    {
        pd = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        LobbyReadyComponent ready = Lobby.Current.GetLobbyComponent<LobbyReadyComponent>();

        ready.OnLobbyReadyEvent += Ready_OnLobbyReadyEvent;
    }

    private void OnDestroy()
    {
        ready.OnLobbyReadyEvent -= Ready_OnLobbyReadyEvent;
    }

    private void Ready_OnLobbyReadyEvent()
    {
        pd.Play();
    }

    public void InvokePlayerTimeline()
    {
        InvokePlayerTimelineClientRpc();
    }

    [ClientRpc]
    private void InvokePlayerTimelineClientRpc()
    {
        StartPlayerTimelineAction?.Invoke();
    }
}
