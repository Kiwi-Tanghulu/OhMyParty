using Cinemachine;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerCamera : CharacterComponent
    {
        protected CinemachineVirtualCamera cam;
        public CinemachineVirtualCamera Camera => cam;

        [SerializeField] private bool nonParent;
        [SerializeField] private bool lookAt;
        [SerializeField] private bool follow;
        public bool LookAt => lookAt;
        public bool Follow => follow;

        [Space]
        [SerializeField] private bool orthographic;

        [Space]
        [SerializeField] private CinemachineBrain.UpdateMethod updateMethod;
        [SerializeField] private CinemachineBrain.BrainUpdateMethod blendUpdateMethod;

        public override void Init(OMG.CharacterController controller)
        {
            base.Init(controller);

            cam = transform.Find("PlayerVCam").GetComponent<CinemachineVirtualCamera>();
            if(lookAt)
                cam.LookAt = controller.transform;
            if(follow)
                cam.Follow = controller.transform;
            if (nonParent)
            {
                transform.SetParent(null);
                controller.OnDestroyEvent += () => Destroy(gameObject);
            }

            if (controller.IsOwner)
            {
                cam.Priority = DEFINE.FOCUSED_PRIORITY;
                Camera.main.orthographic = orthographic;
                CameraManager.Instance.ChangeUpdateMode(updateMethod, blendUpdateMethod);
            }
            else
                cam.Priority = DEFINE.UNFOCUSED_PRIORITY;
        }

        public virtual void SetLookAt(Transform target)
        {
            cam.LookAt = target;
        }

        public virtual void SetFollow(Transform target)
        {
            cam.Follow = target;
        }

        private void OnDestroy()
        {
            Camera.main.orthographic = false;
            CameraManager.Instance.ResetUpdateMode();
        }
    }
}