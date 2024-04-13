using Unity.Netcode;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace OMG.UI.Minigames
{
    public class MinigameUI : NetworkBehaviour
    {
        private ResultPanel resultPanel = null;
        public ResultPanel ResultPanel => resultPanel;

        private UniversalAdditionalCameraData cameraData = null;
        private Camera uiCamera = null;

        private void Awake()
        {
            uiCamera = transform.Find("UICamera").GetComponent<Camera>();
            resultPanel = transform.Find("ResultPanel").GetComponent<ResultPanel>();
        }

        public void Init()
        {
            SettingCameraClientRpc();
        }

        public void Release()
        {
            ReleaseCameraClientRpc();
        }

        [ClientRpc]
        private void SettingCameraClientRpc()
        {
            cameraData = Camera.main.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(uiCamera);
        }

        [ClientRpc]
        private void ReleaseCameraClientRpc()
        {
            cameraData?.cameraStack.Remove(uiCamera);
        }
    }
}
