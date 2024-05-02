using OMG.Lobbies;
using OMG.Minigames;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class MinigameReadyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameNameText;
        [SerializeField] private Image gameImage;
        [SerializeField] private Transform controlKeyInfoContainer;
        [SerializeField] private ControlKeyInfoUI controlKeyInfoPrefab;
        private GameObject container;

        [Space]
        [SerializeField] private Transform readyCheckBoxContainer;
        [SerializeField] private PlayerReadyCheckBox readyCheckBoxPrefab;
        private Dictionary<ulong, PlayerReadyCheckBox> readyCheckBoxDictionary;

        [Space]
        [SerializeField] MinigameListSO minigameList = null;

        private MinigameSO minigameSO;

        private void Start()
        {
            container = transform.Find("Container").gameObject;

            LobbyMinigameComponent lobbyMinigame = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();
            lobbyMinigame.OnMinigameSelectedEvent += MinigameInfoUI_OnMinigameSelectedEvent;
            lobbyMinigame.OnMinigameStartedEvent += LobbyMinigame_OnMinigameStartEvent;

            LobbyReadyComponent lobbyReady = Lobby.Current.GetLobbyComponent<LobbyReadyComponent>();
            lobbyReady.OnPlayerReadyEvent += MinigameInfoUI_OnPlayerReadyEvent;

            Hide();
        }

        private void LobbyMinigame_OnMinigameStartEvent()
        {
            Hide();
        }

        private void MinigameInfoUI_OnMinigameSelectedEvent(int index)
        {
            SetMinigameInfo(minigameList[index]);
        }

        private void MinigameInfoUI_OnPlayerReadyEvent(ulong id)
        {
            if(Lobby.Current.LobbyState == LobbyState.MinigameSelected)
            {
                if(readyCheckBoxDictionary.ContainsKey(id))
                {
                    readyCheckBoxDictionary[id].SetCheck(true);
                }
            }
        }

        public void SetMinigameInfo(MinigameSO minigameSO)
        {
            this.minigameSO = minigameSO;
        }

        public void Display()
        {
            if (minigameSO == null)
                return;

            foreach (Transform controlKey in controlKeyInfoContainer)
                Destroy(controlKey.gameObject);
            foreach (Transform checkBox in readyCheckBoxContainer)
                Destroy(readyCheckBoxContainer.gameObject);
            readyCheckBoxDictionary = new Dictionary<ulong, PlayerReadyCheckBox>();

            gameNameText.text = minigameSO.MinigameName;
            gameImage.sprite = minigameSO.MinigameImage;

            foreach (ControlKeyInfo keyInfo in minigameSO.ControlKeyInfoList)
            {
                ControlKeyInfoUI controlKey = Instantiate(controlKeyInfoPrefab, controlKeyInfoContainer);

                controlKey.DisplayKeyInfo(keyInfo);
            }

            foreach(OMG.Lobbies.PlayerData playerData in Lobby.Current.PlayerDatas)
            {
                PlayerReadyCheckBox checkBox = Instantiate(readyCheckBoxPrefab, readyCheckBoxContainer);
                readyCheckBoxDictionary.Add(playerData.clientID, checkBox);
            }

            container.SetActive(true);
        }

        public void Hide()
        {
            container.SetActive(false);
        }
    }
}