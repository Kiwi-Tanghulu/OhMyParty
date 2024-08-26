using Cinemachine;
using OMG;
using OMG.Interacting;
using OMG.Lobbies;
using OMG.Player;
using OMG.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class LobbyTV : MonoBehaviour, IFocusable, IInteractable
{
    [SerializeField] private CinemachineVirtualCamera focusCam;
    
    [Space]
    [SerializeField] private MinigameSettingUI minigameSettingUI;

    private LobbyMinigameComponent minigameCompo;

    public GameObject CurrentObject => gameObject;

    public UnityEvent<bool> OnFocuesed;

    private void Start()
    {
        minigameCompo = Lobby.Current.GetLobbyComponent<LobbyMinigameComponent>();
    }

    public bool Interact(Component performer, bool actived, Vector3 point = default)
    {
        PlayerController player = performer.GetComponent<PlayerController>();
        if (player == null)
            return false;

        if (player.IsHost == false)
            return false;

        if (minigameCompo.IsStartCycle.Value == true)
            return false;

        minigameSettingUI.Show();
        CameraManager.Instance.ChangeCamera(focusCam, 0f);

        return true;
    }

    public void OnFocusBegin(Vector3 point)
    {
        if (NetworkManager.Singleton.IsHost == false)
            return;

        OnFocuesed?.Invoke(true);
    }

    public void OnFocusEnd()
    {
        if (NetworkManager.Singleton.IsHost == false)
            return;

        OnFocuesed?.Invoke(false);
    }
}
