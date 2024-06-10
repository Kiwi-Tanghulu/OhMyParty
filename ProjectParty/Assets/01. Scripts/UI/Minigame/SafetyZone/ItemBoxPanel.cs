using System;
using OMG.Extensions;
using OMG.Timers;
using OMG.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI.Minigames.SafetyZones
{
    public class ItemBoxPanel : MonoBehaviour
    {
        [SerializeField] OptOption<Color> colorOption = new OptOption<Color>();
    
        private Image itemImage = null;
        private Image outline = null;

        private Timer currentTimer = null;

        private void Awake()
        {
            outline = transform.Find("Outline").GetComponent<Image>();
            itemImage = transform.Find("ItemImage").GetComponent<Image>();
        }

        public void Display(bool active)
        {
            StopAllCoroutines();
            gameObject.SetActive(active);
        }

        public void Init(Timer timer, Sprite itemIcon)
        {
            currentTimer = timer;
            currentTimer.OnValueChangedEvent.AddListener(HandleTimerValueChanged);

            itemImage.sprite = itemIcon;
            SetOutlineColor();
            outline.fillAmount = currentTimer.Ratio;
        }

        private void HandleTimerValueChanged(float ratio)
        {
            outline.fillAmount = currentTimer.Ratio;
            SetOutlineColor();   
        }

        private void SetOutlineColor()
        {
            Color color = colorOption.GetOption(currentTimer.Finished);
            if(outline.color != color)
                outline.color = color;
        }
    }
}
