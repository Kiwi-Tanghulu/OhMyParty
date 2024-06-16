using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TaggerTextEffect : NetworkBehaviour
{
    [SerializeField] private EmotionText emotionTextPrefab;
    [SerializeField] private Vector3 effectPos;
    [SerializeField] private Vector3 effectRotation;

    [ClientRpc]
    public void MakeTextEffectClientRPC(char text)
    {
        EmotionText emotionText = Instantiate(emotionTextPrefab, transform.position + effectPos, Quaternion.Euler(effectRotation));
        emotionText.StartEffect(text);
    }
}
