using Cinemachine;
using OMG.Lobbies;
using OMG.Minigames;
using OMG.Player;
using Steamworks;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

namespace OMG.UI
{
    public class MinigameInfoContainer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI gameNameText;
        [SerializeField] private TextMeshProUGUI gameDescriptionText;
        [SerializeField] private OMGVideoPlayer videoPlayer;

        [Space]
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

        private void Awake()
        {
            readyCheckBoxDictionary = new Dictionary<ulong, PlayerReadyCheckBox>();

            container = transform.Find("Container").gameObject;
        }

        private void Start()
        {
            LobbyMinigameComponent lobbyMinigame = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();
            lobbyMinigame.OnMinigameRouletteChangeEvent += MinigameInfoUI_OnMinigameRouletteChangeEventEvent;
            lobbyMinigame.OnMinigameStartedEvent += LobbyMinigame_OnMinigameStartEvent;

            LobbyReadyComponent lobbyReady = Lobby.Current.GetLobbyComponent<LobbyReadyComponent>();
            lobbyReady.OnPlayerReadyEvent += MinigameInfoUI_OnPlayerReadyEvent;

            Hide();
        }

        public void Show()
        {
            if (minigameSO == null)
                return;

            SetPlayerUI();

            gameObject.SetActive(true);

            videoPlayer.Play(minigameSO.Video, 1f);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void SetMinigameInfo(MinigameSO minigameSO)
        {
            this.minigameSO = minigameSO;

            foreach (Transform controlKey in controlKeyInfoContainer)
                Destroy(controlKey.gameObject);

            gameNameText.text = minigameSO.MinigameName;
            gameDescriptionText.text = minigameSO.MinigameDescription;

            foreach (ControlKeyInfo keyInfo in minigameSO.ControlKeyInfoList)
            {
                ControlKeyInfoUI controlKey = Instantiate(controlKeyInfoPrefab, controlKeyInfoContainer);

                controlKey.DisplayKeyInfo(keyInfo);
            }

            Debug.Log("set minigame info");
        }

        private void SetPlayerUI()
        {
            foreach (var keyValuePair in readyCheckBoxDictionary)
                Destroy(keyValuePair.Value.gameObject);
            readyCheckBoxDictionary = new Dictionary<ulong, PlayerReadyCheckBox>();

            foreach (PlayerController player in Lobby.Current.PlayerContainer.PlayerList)
            {
                PlayerReadyCheckBox checkBox = Instantiate(readyCheckBoxPrefab, readyCheckBoxContainer);
                checkBox.SetPlayerImage(
                    PlayerManager.Instance.RenderTargetPlayerDic[player.OwnerClientId].RenderTexture);
                checkBox.SetNameText(player.name); //
                readyCheckBoxDictionary.Add(player.OwnerClientId, checkBox);
            }
        }

        private void LobbyMinigame_OnMinigameStartEvent()
        {
            Hide();
        }

        private void MinigameInfoUI_OnMinigameRouletteChangeEventEvent(int index)
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
    }
}