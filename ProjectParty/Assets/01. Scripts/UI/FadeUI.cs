using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class FadeUI : MonoBehaviour
    {
        [SerializeField] private float fadeTime;
        [SerializeField] private float fadeDelay;

        private Material mat_1;
        private Material mat_2;
        private string alphaClipHash = "_AlphaClip";
        private string isOutHash = "_IsOut";

        private void Awake()
        {
            mat_1 = transform.Find("Image_1").GetComponent<Image>().material;
            mat_2 = transform.Find("Image_2").GetComponent<Image>().material;
        }

        public void Play(bool isOut)
        {
            StartCoroutine(FadeCo(isOut));
        }

        private IEnumerator FadeCo(bool _isOut)
        {
            float start = _isOut ? 1f : 0f;
            float end = _isOut ? 0f : 1f;
            float isOut = _isOut ? 1f : 0f;
            Material mat_1 = _isOut ? this.mat_2 : this.mat_1;
            Material mat_2 = _isOut ? this.mat_1 : this.mat_2;

            mat_1.SetFloat(isOutHash, isOut);
            mat_2.SetFloat(isOutHash, isOut);

            StartCoroutine(Lerping(mat_1, start, end));
            yield return new WaitForSeconds(fadeDelay);
            StartCoroutine(Lerping(mat_2, start, end));
        }

        private IEnumerator Lerping(Material mat, float start, float end)
        {
            float current = 0f;
            float percent = 0f;

            while (percent <= 1f)
            {
                current = Mathf.Lerp(start, end, percent);
                mat.SetFloat(alphaClipHash, current);

                percent += Time.deltaTime / fadeTime;

                yield return null;
            }

            mat.SetFloat(alphaClipHash, end);
        }
    }
}