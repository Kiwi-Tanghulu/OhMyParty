using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace OMG.UI.Minigames
{
    public class MinigameUI : MonoBehaviour
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
            cameraData = Camera.main.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(uiCamera);
        }

        public void Release()
        {
            cameraData?.cameraStack.Remove(uiCamera);
        }
    }
}
