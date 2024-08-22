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

        chatUI.InputField.onSubmit.AddListener(OnSubmit);
        chatUI.OnHide += () => isChatting = false;
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        playInput.OnChatEvent -= Chatting;
        uiInput.OnChatEvent -= Chatting;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsChatting)
            UIManager.Instance.HidePanel();
    }

    private void Chatting()
    {
        if (isChatting == false)
            StartChat();
    }

    private void StartChat()
    {
        if (UIManager.Instance.StackCount > 0)
            return;

        isChatting = true;
        chatUI.Show();
    }

    public void Send(ulong senderID, string message)
    {
        //chatUI.CreateChat("Test", message);
        SendServerRpc(senderID, new FixedString64Bytes(message));
    }

    [ServerRpc(RequireOwnership = false)]
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

        if (IsChatting == false)
            chatUI.OnlyShow();
    }

    private void OnSubmit(string message)
    {
        if (IsChatting == false)
            return;

        isChatting = false;
        UIManager.Instance.HidePanel();

        if (message == "")
            return;

        chatUI.OnlyShow();
        Send(NetworkManager.LocalClientId, message);
    }
}