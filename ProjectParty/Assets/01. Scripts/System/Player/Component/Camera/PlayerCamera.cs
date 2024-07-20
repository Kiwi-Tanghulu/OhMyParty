using Cinemachine;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerCamera : CharacterComponent
    {
        protected CinemachineVirtualCamera cam;

        [SerializeField] private bool nonParent;
        [SerializeField] private bool lookAt;
        [SerializeField] private bool follow;

        public override void Init(OMG.CharacterController controller)
        {
            base.Init(controller);

            cam = transform.Find("PlayerVCam").GetComponent<CinemachineVirtualCamera>();
            if(lookAt)
                cam.LookAt = controller.transform;
            if(follow)
                cam.Follow = controller.transform;
            if (nonParent)
                transform.SetParent(null);

            if (controller.IsOwner)
                cam.Priority = DEFINE.FOCUSED_PRIORITY;
            else
                cam.Priority = DEFINE.UNFOCUSED_PRIORITY;
        }
    }
}