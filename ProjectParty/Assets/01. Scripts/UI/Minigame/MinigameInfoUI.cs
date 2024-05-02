using OMG.Lobbies;
using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class MinigameInfoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameNameText;
        [SerializeField] private Image gameImage;
        [SerializeField] private Transform controlKeyInfoContainer;
        [SerializeField] private ControlKeyInfoUI controlKeyInfoPrefab;
        private GameObject container;

        [Space]
        [SerializeField] MinigameListSO minigameList = null;

        private MinigameSO minigameSO;

        private void Start()
        {
            container = transform.Find("Container").gameObject;
            container.SetActive(false);

            Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>().OnMinigameSelectedEvent += MinigameInfoUI_OnMinigameSelectedEvent;
        }

        private void MinigameInfoUI_OnMinigameSelectedEvent(int index)
        {
            Debug.Log(minigameList);
            SetMinigameInfo(minigameList[index]);
        }

        public void SetMinigameInfo(MinigameSO minigameSO)
        {
            this.minigameSO = minigameSO;
        }

        public void DispalyMinigameInfo()
        {
            gameNameText.text = minigameSO.MinigameName;
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

            container.SetActive(true);
        }
    }
}