using OMG.Minigames;
using OMG.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MinigameSettingSlot : MinigameSlot, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private CheckBox checkBox;

    private bool isSelected = false;

    public UnityEvent<MinigameSO, bool> OnSelectedEvent;

    public override void Init()
    {
        base.Init();

        Unhover();
    }

    public override void Hide()
    {
        base.Hide();

        if(isSelected == false)
            Unhover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select(!isSelected);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(isSelected == false)
            Hover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(isSelected == false)
            Unhover();
    }

    public void Select(bool isSelected)
    {
        this.isSelected = isSelected;

        checkBox.SetCheck(this.isSelected);

        if (this.isSelected)
            Hover();
        else
            Unhover();

        OnSelectedEvent?.Invoke(MinigameSO, this.isSelected);
    }
}
