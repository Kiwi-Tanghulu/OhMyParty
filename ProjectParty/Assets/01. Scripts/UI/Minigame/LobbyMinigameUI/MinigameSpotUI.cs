using Cinemachine;
using OMG.Extensions;
using OMG.Inputs;
using OMG.Lobbies;
using UnityEngine;

namespace OMG.UI
{
    public class MinigameSpotUI : UIObject
    {
        [SerializeField] private MinigamePickUI pick;
        public MinigamePickUI PickUI => pick;
        [SerializeField] private MinigameInfoUI info;
        public MinigameInfoUI InfoContainer => info;

        [Space]
        [SerializeField] private CinemachineVirtualCamera focusCam;

        public override void Init()
        {
            base.Init();

            Hide();
        }

        public override void Show()
        {
            gameObject.SetActive(true);

            CameraManager.Instance.ChangeCamera(focusCam);
            pick.Show();
        }
    }
}