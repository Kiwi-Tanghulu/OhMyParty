using Cinemachine;

namespace OMG.Player
{
    public class PlayerCamera : CharacterComponent
    {
        protected CinemachineVirtualCamera cam;

        public override void Init(OMG.CharacterController controller)
        {
            base.Init(controller);

            cam = transform.Find("PlayerVCam").GetComponent<CinemachineVirtualCamera>();
            cam.LookAt = controller.transform;

            if (controller.IsOwner)
                cam.Priority = DEFINE.FOCUSED_PRIORITY;
            else
                cam.Priority = DEFINE.UNFOCUSED_PRIORITY;
        }
    }
}