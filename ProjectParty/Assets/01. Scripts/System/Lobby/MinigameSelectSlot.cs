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

    private bool isChecked = false;

    public override void Init()
    {
        base.Init();

        Unhover();
    }

    public override void Hide()
    {
        base.Hide();

        if(isChecked == false)
            Unhover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isChecked = !isChecked;
        checkBox.SetCheck(isChecked);

        if (isChecked)
            Hover();
        else
            Unhover();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isChecked == false)
            Hover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isChecked == false)
            Unhover();
    }
}
