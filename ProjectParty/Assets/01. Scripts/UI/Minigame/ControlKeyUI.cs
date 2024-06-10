using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class ControlKeyUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI text;

        public void SetControlKey(ControlKey controlKey)
        {
            Vector2 resolution = image.rectTransform.sizeDelta;
            resolution.x *= controlKey.WidthRatio;

            if (controlKey.KeyImage != null)
            {
                image.sprite = controlKey.KeyImage;

                float ratio = image.sprite.rect.width / image.sprite.rect.height;
                
                resolution.x *= ratio;
            }

            image.rectTransform.sizeDelta = resolution;

            text.text = controlKey.KeyText;
        }
    }
}