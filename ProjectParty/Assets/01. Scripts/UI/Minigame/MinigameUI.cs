using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace OMG.UI.Minigames
{
    public class MinigameUI : MonoBehaviour
    {
        private ResultPanel resultPanel = null;
        public ResultPanel ResultPanel => resultPanel;

        private ScorePanel scorePanel = null;
        public ScorePanel ScorePanel => scorePanel;

        private UniversalAdditionalCameraData cameraData = null;
        private Camera uiCamera = null;

        private void Awake()
        {
            uiCamera = transform.Find("UICamera").GetComponent<Camera>();
            resultPanel = transform.Find("ResultPanel")?.GetComponent<ResultPanel>();
            scorePanel = transform.Find("ScorePanel")?.GetComponent<ScorePanel>();
        }

        private void Start()
        {
            SettingCamera();
        }

        private void OnDestroy()
        {
            ReleaseCamera();
        }

        private void SettingCamera()
        {
            cameraData = Camera.main.GetUniversalAdditionalCameraData();
            cameraData.cameraStack.Add(uiCamera);
        }

        private void ReleaseCamera()
        {
            cameraData?.cameraStack.Remove(uiCamera);
        }
    }
}
