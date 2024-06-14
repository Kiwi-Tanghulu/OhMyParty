using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ComicHitEffect : MonoBehaviour
{
    private TextMeshPro tmp;

    private void Awake()
    {
        tmp = GetComponent<TextMeshPro>();
    }
    public void SetText(string text)
    {
        tmp.text = text;
    }
}
