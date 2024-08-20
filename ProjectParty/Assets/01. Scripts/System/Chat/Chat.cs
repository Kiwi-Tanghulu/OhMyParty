using OMG.Extensions;
using OMG.Inputs;
using OMG.Lobbies;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Chat : NetworkBehaviour
{
    [SerializeField] private ChatUI chatUI;

    private bool isChatting;
    public bool IsChatting => isChatting;

    private void StartChat()
    {
        if (InputManager.Enable == false)
            return;

        chatUI.Show();
    }

    public void Send(ulong senderID, string message)
    {
        SendServerRpc(senderID, new FixedString64Bytes(message));
        isChatting = false;
    }

    [ServerRpc]
    private void SendServerRpc(ulong senderID, FixedString64Bytes message)
    {
        SendClientRpc(senderID, message);
    }

    [ClientRpc]
    private void SendClientRpc(ulong senderID, FixedString64Bytes message)
    {
        int findID = Lobby.Current.PlayerDatas.Find(out PlayerData data, (d) =>
        {
            return d.ClientID == senderID;
        });
        if (findID == -1)
            return;

        string senderName = data.Nickname;
        chatUI.ShowChat(senderName, message.Value);
    }

    private void EndChat()
    {
        chatUI.Hide();
    }
}