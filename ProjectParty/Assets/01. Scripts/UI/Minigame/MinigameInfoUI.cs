using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class MinigameInfoUI : MonoBehaviour
    {
        [SerializeField] private Image gameImage;
        [SerializeField] private Transform controlKeyInfoContainer;
        [SerializeField] private ControlKeyInfoUI controlKeyInfoPrefab;

        public void DispalyMinigameInfo(MinigameSO minigameSO)
        {
            gameImage.sprite = minigameSO.MinigameImage;

            foreach (Transform controlKey in controlKeyInfoContainer)
            {
                Destroy(controlKey.gameObject);
            }

            foreach(ControlKeyInfo keyInfo in minigameSO.ControlKeyInfoList)
            {
                ControlKeyInfoUI controlKey = Instantiate(controlKeyInfoPrefab, controlKeyInfoContainer);

                controlKey.DisplayKeyInfo(keyInfo);
            }
        }
    }
}