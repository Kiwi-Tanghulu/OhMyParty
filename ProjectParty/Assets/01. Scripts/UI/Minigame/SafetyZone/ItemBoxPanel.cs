using OMG.Timers;
using OMG.Utility;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI.Minigames.SafetyZones
{
    public class ItemBoxPanel : MonoBehaviour
    {
        [SerializeField] float yOffset = 150f;
        [SerializeField] OptOption<Color> colorOption = new OptOption<Color>();
    
        private Image itemImage = null;
        private Image outline = null;

        private Camera mainCamera = null;
        private Timer currentTimer = null;

        private void Awake()
        {
            outline = transform.Find("Outline").GetComponent<Image>();
            itemImage = transform.Find("ItemImage").GetComponent<Image>();

            mainCamera = Camera.main;
        }

        private void Start()
        {
            Release();
        }

        public void Init(Vector3 point, Timer timer, Sprite itemIcon)
        {
            transform.position = mainCamera.WorldToScreenPoint(point) + Vector3.up * yOffset;

            currentTimer = timer;
            currentTimer.OnValueChangedEvent.AddListener(HandleTimerValueChanged);
            
            itemImage.sprite = itemIcon;
            SetOutlineColor();
            outline.fillAmount = currentTimer.Ratio;

            gameObject.SetActive(true);
        }

        public void Release()
        {
            gameObject.SetActive(false);

            itemImage.sprite = null;
            outline.fillAmount = 1f;

            currentTimer?.OnValueChangedEvent.RemoveListener(HandleTimerValueChanged);
            currentTimer = null;
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
