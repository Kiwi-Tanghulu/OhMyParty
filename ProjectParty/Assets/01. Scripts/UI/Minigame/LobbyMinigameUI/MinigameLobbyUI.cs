using Cinemachine;
using OMG.Lobbies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI
{
    public class MinigameLobbyUI : MonoBehaviour
    {
        [SerializeField] private MinigameRouletteContainer roulette;
        [SerializeField] private MinigameInfoContainer info;

        [Space]
        [SerializeField] private CinemachineVirtualCamera focusCam;

        private void Start()
        {
            LobbyMinigameComponent lobbyMinigame = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();
            lobbyMinigame.OnMinigameSelectingEvent += LobbyMinigame_OnMinigameSelectingEvent;
        }

        private void LobbyMinigame_OnMinigameSelectingEvent()
        {
            CameraManager.Instance.ChangeCamera(focusCam);

            roulette.Show();
        }
    }
}