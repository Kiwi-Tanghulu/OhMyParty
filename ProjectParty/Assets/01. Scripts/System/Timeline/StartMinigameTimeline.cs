using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class StartMinigameTimeline : MonoBehaviour
{
    public event Action StartPlayerTimelineAction;

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
