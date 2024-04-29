using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace OMG
{
    public class Screenshot : MonoBehaviour
    {
        [SerializeField] private Image showImage;

        private Texture2D originalScreenshot;
        private Texture2D correctionScreenshot;

        public void TakeScreenShot()
        {
            StartCoroutine(TakeScreenshotCo());
        }

        private IEnumerator TakeScreenshotCo()
        {
            yield return new WaitForEndOfFrame();

            originalScreenshot = ScreenCapture.CaptureScreenshotAsTexture();
            correctionScreenshot = new Texture2D(originalScreenshot.width, originalScreenshot.height, TextureFormat.RGB24, false);
            correctionScreenshot.SetPixels(originalScreenshot.GetPixels());
            correctionScreenshot.Apply();

            Sprite screenshot = Sprite.Create(correctionScreenshot, new Rect(0f, 0f, correctionScreenshot.width, correctionScreenshot.height),
                new Vector2(0.5f, 0.5f));

            showImage.sprite = screenshot;
        }
    }
}
