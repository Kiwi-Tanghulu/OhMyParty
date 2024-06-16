using Cinemachine;
using OMG.Extensions;
using OMG.Inputs;
using OMG.Lobbies;
using UnityEngine;

namespace OMG.UI
{
    public class MinigameLobbyUI : UIObject
    {
        [SerializeField] private MinigameRouletteContainer roulette;
        [SerializeField] private MinigameInfoContainer info;

        [Space]
        [SerializeField] private CinemachineVirtualCamera focusCam;

        public override void Init()
        {
            base.Init();

            LobbyMinigameComponent lobbyMinigame = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();
            lobbyMinigame.OnMinigameSelectingEvent += LobbyMinigame_OnMinigameSelectingEvent;
            lobbyMinigame.OnMinigameSelectedEvent += LobbyMinigame_OnMinigameSelectedEvent;
            lobbyMinigame.OnMinigameStartedEvent += LobbyMinigame_OnMinigameStartedEvent;

            Hide();
        }

        private void LobbyMinigame_OnMinigameSelectingEvent()
        {
            Show();

            //should make delay
            CameraManager.Instance.ChangeCamera(focusCam, 2f, null, () => roulette.Show());

            InputManager.ChangeInputMap(InputMapType.UI);
            InputManager.SetInputEnable(false);
        }

        private void LobbyMinigame_OnMinigameSelectedEvent(int obj)
        {
            roulette.Hide();
        }
        private void LobbyMinigame_OnMinigameStartedEvent()
        {
            Hide();
        }


        public override void Show()
        {
            gameObject.SetActive(true);
        }
    }
}