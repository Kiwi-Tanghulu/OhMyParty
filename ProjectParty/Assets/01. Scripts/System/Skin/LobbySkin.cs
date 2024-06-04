using Cinemachine;
using OMG.Lobbies;
using UnityEngine;

namespace OMG.Skins
{
    public class LobbySkin : Skin
    {
        [SerializeField] Transform minigameSpotTrm = null;

        [Space]
        [SerializeField] private CinemachineVirtualCamera cam;

        public override void Init()
        {
            base.Init();

            Transform minigameSpot = Lobby.Current.transform.Find("MinigameSpot");
            minigameSpot.position = minigameSpotTrm.position;
            minigameSpot.rotation = minigameSpotTrm.rotation;

            CameraManager.Instance.ChangeCamera(cam);
        }

        public override void Release()
        {
            base.Release();
        }
    }
}
