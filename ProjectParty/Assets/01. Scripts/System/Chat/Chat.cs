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
    [SerializeField] private PlayInputSO playInput;
    [SerializeField] private UIInputSO uiInput;

    [Space]
    [SerializeField] private ChatUI chatUI;

    private bool isChatting;
    public bool IsChatting => isChatting;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        playInput.OnChatEvent += Chatting;
        uiInput.OnChatEvent += Chatting;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        playInput.OnChatEvent -= Chatting;
        uiInput.OnChatEvent -= Chatting;
    }

    //private void Start()
    //{
    //    playInput.OnChatEvent += Chatting;
    //    uiInput.OnChatEvent += Chatting;

    //    InputManager.ChangeInputMap(InputMapType.Play);
    //}

    private void Chatting()
    {
        Debug.Log("chatting");
        if (isChatting == false)
            StartChat();
        else
            EndChat();
    }

    private void StartChat()
    {
        Debug.Log(UIManager.Instance.StackCount);
        if (UIManager.Instance.StackCount > 0)
            return;

        isChatting = true;
        chatUI.Show();
    }

    public void Send(ulong senderID, string message)
    {
        chatUI.CreateChat("Test", message);
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
        chatUI.CreateChat(senderName, message.Value);
    }

    private void EndChat()
    {
        isChatting = false;
        UIManager.Instance.HidePanel();

        if (chatUI.Message == "")
            return;

        chatUI.OnlyShow();
        Send(0, chatUI.Message);
    }
}