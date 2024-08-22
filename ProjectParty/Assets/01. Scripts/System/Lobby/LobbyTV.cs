using Cinemachine;
using OMG;
using OMG.Interacting;
using OMG.Player;
using OMG.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LobbyTV : MonoBehaviour, IFocusable, IInteractable
{
    [SerializeField] private CinemachineVirtualCamera focusCam;
    
    [Space]
    [SerializeField] private MinigameSettingUI minigameSettingUI;

    public GameObject CurrentObject => gameObject;

    public bool Interact(Component performer, bool actived, Vector3 point = default)
    {
        PlayerController player = performer.GetComponent<PlayerController>();
        if (player == null)
            return false;

        if (player.IsHost == false)
            return false;

        minigameSettingUI.Show();
        CameraManager.Instance.ChangeCamera(focusCam, 0f);

        return true;
    }

    public void OnFocusBegin(Vector3 point)
    {
        
    }

    public void OnFocusEnd()
    {
        
    }
}
