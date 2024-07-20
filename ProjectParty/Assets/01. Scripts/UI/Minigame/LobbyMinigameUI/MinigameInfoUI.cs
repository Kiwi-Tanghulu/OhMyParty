using Cinemachine;
using OMG.Extensions;
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
    public class MinigameInfoUI : UIObject
    {
        [SerializeField] private TextMeshProUGUI gameNameText;
        [SerializeField] private TextMeshProUGUI gameDescriptionText;
        [SerializeField] private OMGVideoPlayer videoPlayer;

        [Space]
        [SerializeField] private Transform controlKeyInfoContainer;
        [SerializeField] private ControlKeyInfoUI controlKeyInfoPrefab;

        [Space]
        [SerializeField] private Transform readyCheckBoxContainer;
        [SerializeField] private PlayerReadyCheckBox readyCheckBoxPrefab;
        private Dictionary<ulong, PlayerReadyCheckBox> readyCheckBoxDictionary;

        [Space]
        [SerializeField] MinigameListSO minigameList = null;

        private MinigameSO minigameSO;

        public override void Init()
        {
            base.Init();

            readyCheckBoxDictionary = new Dictionary<ulong, PlayerReadyCheckBox>();
            LobbyMinigameComponent lobbyMinigame = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();
            lobbyMinigame.OnMinigameSelectedEvent += MinigameInfoUI_OnMinigameSelecteEvent;
            lobbyMinigame.OnMinigameStartedEvent += LobbyMinigame_OnMinigameStartEvent;

            LobbyReadyComponent lobbyReady = Lobby.Current.GetLobbyComponent<LobbyReadyComponent>();
            lobbyReady.OnPlayerReadyEvent += MinigameInfoUI_OnPlayerReadyEvent;
        }

        public override void Show()
        {
            if (minigameSO == null)
                return;

            base.Show();

            SetMinigameUI();
            SetPlayerUI();

            Debug.Log(1);
            videoPlayer.Play(minigameSO.VideoURL, 1f);
        }

        public void SetMinigameSO(MinigameSO minigameSO)
        {
            this.minigameSO = minigameSO;
        }

        private void SetMinigameUI()
        {
            foreach (Transform controlKey in controlKeyInfoContainer)
                Destroy(controlKey.gameObject);

            gameNameText.text = minigameSO.MinigameName;
            gameDescriptionText.text = minigameSO.MinigameDescription;

            foreach (ControlKeyInfo keyInfo in minigameSO.ControlKeyInfoList)
            {
                ControlKeyInfoUI controlKey = Instantiate(controlKeyInfoPrefab, controlKeyInfoContainer);

                controlKey.DisplayKeyInfo(keyInfo);
            }
        }

        private void SetPlayerUI()
        {
            Debug.Log("set player ui");
            foreach(Transform checkboxTrm in readyCheckBoxContainer)
            {
                if(checkboxTrm.TryGetComponent<PlayerReadyCheckBox>(out PlayerReadyCheckBox checkBox))
                {
                    Destroy(checkBox.gameObject);
                }
            }
            //foreach (var keyValuePair in readyCheckBoxDictionary)
            //    Destroy(keyValuePair.Value.gameObject);
            readyCheckBoxDictionary = new Dictionary<ulong, PlayerReadyCheckBox>();

            foreach (PlayerController player in Lobby.Current.PlayerContainer.PlayerList)
            {
                PlayerReadyCheckBox checkBox = Instantiate(readyCheckBoxPrefab, readyCheckBoxContainer);
                checkBox.SetPlayerImage(
                    PlayerManager.Instance.RenderTargetPlayerDic[player.OwnerClientId].RenderTexture);

                Lobby.Current.PlayerDatas.Find(out OMG.Lobbies.PlayerData data, (data) => data.ClientID == player.OwnerClientId);
                checkBox.SetNameText(data.Nickname);

                readyCheckBoxDictionary.Add(player.OwnerClientId, checkBox);
            }
        }

        private void LobbyMinigame_OnMinigameStartEvent()
        {
            Hide();
        }

        private void MinigameInfoUI_OnMinigameSelecteEvent(int index)
        {
            SetMinigameSO(minigameList[index]);
            Show();
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