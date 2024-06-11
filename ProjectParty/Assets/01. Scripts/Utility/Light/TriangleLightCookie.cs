using UnityEngine;

namespace OMG.Lightings
{
    public class TriangleLightCookie : MonoBehaviour
    {
        [SerializeField] Material cookieMaker = null;
        [SerializeField, Range(0f, 1f)] float value = 0f;
        private float currentValue = -1f;

        private Light target = null;
        private Texture2D texture = null;

        private void Awake()
        {
            Destroy(this);
        }

        private void OnValidate()
        {
            if(target == null)
                target = GetComponent<Light>();

            if(value == currentValue && target.cookie != null)
                return;

            if(cookieMaker == null)
                return;
            
            UpdateCookie();
        }

        [ContextMenu("Update Cookie")]
        private void UpdateCookie()
        {
            currentValue = value;

            RenderTexture renderTexture = new RenderTexture(256, 256, 0);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();

            cookieMaker.SetFloat("_Scale", currentValue);
            Graphics.Blit(null, renderTexture, cookieMaker);

            texture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGBA32, false);
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            RenderTexture.active = null;

            target.cookie = texture;
        }
    }
}
