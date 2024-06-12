using UnityEngine;

namespace OMG.Lightings
{
    public class SectorLightCookie : MonoBehaviour
    {
        public enum QualityLevel
        {
            x64 = 2 << 6,
            x128 = 2 << 7,
            x256 = 2 << 8,
            x512 = 2 << 9,
            x1024 = 2 << 10,
            x2048 = 2 << 11,
            x4096 = 2 << 12
        }

        [SerializeField] Material cookieMaker = null;
        [SerializeField] QualityLevel level = QualityLevel.x1024;
        [SerializeField, Range(0f, 360f)] float angle = 0f;
        [SerializeField] Vector2 forward = Vector2.up;
        private float currentAngle = -1f;
        private QualityLevel currentLevel = 0;

        private Light target = null;

        private void Awake()
        {
            Destroy(this);
        }

        private void OnValidate()
        {
            if(target == null)
                target = GetComponent<Light>();

            if(angle == currentAngle && currentLevel == level && target.cookie != null)
                return;

            if(cookieMaker == null)
                return;
            
            UpdateCookie();
        }

        [ContextMenu("Update Cookie")]
        private void UpdateCookie()
        {
            Material material = Instantiate(cookieMaker);
            currentAngle = angle;
            currentLevel = level;

            RenderTexture renderTexture = new RenderTexture((int)currentLevel, (int)currentLevel, 0);
            renderTexture.Create();

            material.SetFloat("_Angle", currentAngle);
            material.SetVector("_Forward", forward);

            
            Graphics.Blit(null, renderTexture, material);

            RenderTexture.active = renderTexture;
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            RenderTexture.active = null;

            target.cookie = texture;
        }
    }
}
