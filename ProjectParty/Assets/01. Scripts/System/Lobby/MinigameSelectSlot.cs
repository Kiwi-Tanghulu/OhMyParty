using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinigameSelectSlot : MonoBehaviour
{
    [SerializeField] private Image minigameImage;
    [SerializeField] private TextMeshProUGUI minigameNameText;

    private MinigameSO minigameSO;

    public void SetMinigameSO(MinigameSO minigameSO)
    {
        this.minigameSO = minigameSO;

        minigameImage.sprite = minigameSO.MinigameImage;
        minigameNameText.text = minigameSO.MinigameName;
    }
}
