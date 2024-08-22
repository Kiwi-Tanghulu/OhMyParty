using OMG.Minigames;
using OMG.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinigameSelectSlot : MinigameSlot, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CheckBox checkBox;

    public override void Init()
    {
        base.Init();

        Unhover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        checkBox.SetCheck(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Hover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Unhover();
    }
}
