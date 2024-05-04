using Cinemachine;
using OMG.Lobbies;
using OMG.Minigames;
using Steamworks;
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
        [SerializeField] private CinemachineVirtualCamera focusCam;

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
            lobbyMinigame.OnMinigameSelectedEvent += MinigameInfoUI_OnMinigameSelectedEvent;
            lobbyMinigame.OnMinigameStartedEvent += LobbyMinigame_OnMinigameStartEvent;

            LobbyReadyComponent lobbyReady = Lobby.Current.GetLobbyComponent<LobbyReadyComponent>();
            lobbyReady.OnPlayerReadyEvent += MinigameInfoUI_OnPlayerReadyEvent;

            LobbyCutSceneComponent lobbyCutScene = Lobby.Current.GetLobbyComponent<LobbyCutSceneComponent>();
            lobbyCutScene.CutSceneEvents[LobbyCutSceneState.StartFinish] += LobbyCutScene_OnStartFinish;

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

        private void LobbyCutScene_OnStartFinish()
        {
            CameraManager.Instance.ChangeCamera(focusCam);

            Display();
        }

        public void SetMinigameInfo(MinigameSO minigameSO)
        {
            this.minigameSO = minigameSO;
        }

        public void Display()
        {
            if (minigameSO == null)
                return;

            Debug.Log(1);

            foreach (Transform controlKey in controlKeyInfoContainer)
                Destroy(controlKey.gameObject);
            foreach (var keyValuePair in readyCheckBoxDictionary)
                Destroy(keyValuePair.Value.gameObject);
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