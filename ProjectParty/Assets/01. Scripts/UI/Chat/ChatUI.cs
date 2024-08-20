using OMG.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class ChatUI : UIObject
{
    [SerializeField] private ChatText textPrefab;
    [SerializeField] private Transform textContainer;
    [SerializeField] private int maxTextCount;

    public void ShowChat(string senderName, string message)
    {
        ChatText text = Instantiate(textPrefab, textContainer);
        text.SetText(senderName, message);

        if (textContainer.childCount > maxTextCount)
        {
            Destroy(textContainer.GetChild(0));
        }
    }
}
