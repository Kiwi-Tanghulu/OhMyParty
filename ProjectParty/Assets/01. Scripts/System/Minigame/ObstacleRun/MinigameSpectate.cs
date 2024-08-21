using Cinemachine;
using OMG;
using OMG.Extensions;
using OMG.Inputs;
using OMG.Minigames;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameSpectate : MonoBehaviour
{
    [SerializeField] private PlayInputSO input;

    [Space]
    [SerializeField] private bool usePlayerCamera;
    [DrawIf("usePlayerCamera", false)]
    [SerializeField] private List<CinemachineVirtualCamera> cameras;

    private int currentCamIndex;

    public void StartSpectate()
    {
        Debug.Log("start spectate");

        input.OnScrollEvent += HandleOnScroll;

        if(usePlayerCamera)
        {
            PlayableMinigame minigame = MinigameManager.Instance.CurrentMinigame as PlayableMinigame;
            if(minigame != null)
            {
                cameras.Clear();
                foreach (var player in minigame.PlayerDictionary.Values)
                {
                    PlayerCamera cam = player.GetCharacterComponent<PlayerCamera>();
                    if(cam != null)
                    {
                        cameras.Add(cam.Cam);
                    }
                }
            }
        }

        if(cameras.Count > 0)
        {
            currentCamIndex = 0;
            ChangeSpectateCamera(cameras[currentCamIndex]);
        }
    }

    public void StartSpectateWithDelay(float delay)
    {
        StartCoroutine(this.DelayCoroutine(delay, StartSpectate));
    }

    public void ChangeSpectateCamera(CinemachineVirtualCamera cam)
    {
        CameraManager.Instance.ChangeCamera(cam);
    }

    private void HandleOnScroll(int value)
    {
        if (cameras.Count <= 0)
            return;

        currentCamIndex = (currentCamIndex + value + cameras.Count) % cameras.Count;

        ChangeSpectateCamera(cameras[currentCamIndex]);
    }

    private void OnDestroy()
    {
        input.OnScrollEvent -= HandleOnScroll;
    }
}
