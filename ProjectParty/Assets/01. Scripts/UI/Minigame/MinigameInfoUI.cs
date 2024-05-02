using OMG.Input;
using OMG.Lobbies;
using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

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
            Lobby.Current.GetLobbyComponent<LobbyReadyComponent>().OnPlayerReadyEvent += MinigameInfoUI_OnPlayerReadyEvent;
        }

        private void MinigameInfoUI_OnPlayerReadyEvent(ulong id)
        {
            switch (Lobby.Current.LobbyState)
            {
                case LobbyState.Community: // Ŀ�´�Ƽ ������ �� ���� �Ǹ� �̴ϰ��� ���� ����
                case LobbyState.MinigameFinished: // �� �� �̴ϰ����� ���� ������ ���� ��������
                case LobbyState.MinigameSelected: // �̴ϰ��� ���õ� ������ �� ���� �Ǹ� �̴ϰ��� ����
                    // ����ٰ� �ƾ� �������ְ�
                    Debug.Log(123);
                    break;
            }
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