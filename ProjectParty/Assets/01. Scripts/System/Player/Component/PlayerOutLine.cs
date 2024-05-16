using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOutLine : MonoBehaviour
{
    private Outline outLine = null;


    private void Awake()
    {
        outLine = gameObject.AddComponent<Outline>();
        outLine.OutlineMode = Outline.Mode.OutlineHidden;
    }

    public void SettingOutLine(int colorIndex)
    {
        Debug.Log("¿”¡ÿº∫ lol");
        outLine.OutlineMode = Outline.Mode.OutlineAll;
        outLine.OutlineColor = PlayerManager.Instance.GetPlayerColor(colorIndex);
        outLine.OutlineWidth = 2.5f;
    }
}
