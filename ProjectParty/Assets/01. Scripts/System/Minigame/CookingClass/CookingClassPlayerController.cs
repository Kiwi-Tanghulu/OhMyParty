using Cinemachine;
using OMG.Player;
using UnityEngine;

namespace OMG.Minigames.CookingClass
{
    public class CookingClassPlayerController : PlayerController
    {
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if(IsOwner == false)
                return;

            Transform camTrm = MinigameManager.Instance.CurrentMinigame.transform.Find("MinigameVCam");
            camTrm.GetComponent<CinemachineVirtualCamera>().Follow = transform;
        }
    }
}
