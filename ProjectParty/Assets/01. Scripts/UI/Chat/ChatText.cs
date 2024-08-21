using DG.Tweening;
using OMG.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChatText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    [Space]
    [SerializeField] private float enableTime;
    [SerializeField] private float disableTime;

    public void SetText(string senderName, string message)
    {
        text.text = $"- [{senderName}] : {message}";

        //StartCoroutine(this.DelayCoroutine(enableTime, () =>
        //{
        //    text.DOFade(0f, disableTime).
        //    OnComplete(() =>
        //    {
        //        Destroy(gameObject);
        //    });
        //}));
    }
}
