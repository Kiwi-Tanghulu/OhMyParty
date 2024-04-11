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

    private void Start()
    {
        pd = GetComponent<PlayableDirector>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Play");
            pd.Play();
        }
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
