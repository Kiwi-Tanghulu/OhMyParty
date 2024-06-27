using OMG.Minigames;
using TinyGiantStudio.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OMG.UI
{
    public class MinigameSlot : UIObject, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private MinigameSO minigameSO;
        public MinigameSO MinigameSO => minigameSO;

        [Space]
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Image minigameImage;

        [Space]
        [SerializeField] private GameObject showImage;
        [SerializeField] private GameObject hideImage;

        [Space]
        [SerializeField] private Image frameImage;
        [SerializeField] private Color hoverColor;
        [SerializeField] private Color unhoverColor;

        [Space]
        public UnityEvent<MinigameSlot> OnSelectedEvent;
        public UnityEvent OnHoverEvent;
        public UnityEvent OnUnhoverEvent;

        private int showHash = Animator.StringToHash("show");
        private int hideHash = Animator.StringToHash("hide");
        private int showcContentHash = Animator.StringToHash("showContent");
        private int hidecContentHash = Animator.StringToHash("hideContent");
        private Animator anim;

        public bool IsSelectable;

        public override void Init()
        {
            base.Init();

            anim = GetComponent<Animator>();

            frameImage.color = unhoverColor;
        }

        public void SetMinigameSO(MinigameSO minigameSO)
        {
            this.minigameSO = minigameSO;

            titleText.text = minigameSO.MinigameName;
            if(minigameSO.MinigameImage != null)
                minigameImage.sprite = minigameSO.MinigameImage;
        }

        public void Selected()
        {
            OnHoverEvent?.Invoke();
        }

        public void Deselected()
        {
            OnUnhoverEvent?.Invoke();
        }

        public void ShowContent(bool value)
        {
            showImage.SetActive(value);
            hideImage.SetActive(!value);

            if (value)
                anim.SetTrigger(showcContentHash);
            else
                anim.SetTrigger(hidecContentHash);
        }

        public override void Show()
        {
            anim.SetTrigger(showHash);
        }

        public override void Hide()
        {
            anim.SetTrigger(hideHash);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!IsSelectable)
                return;

            frameImage.color = hoverColor;
            OnHoverEvent?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!IsSelectable)
                return;

            frameImage.color = unhoverColor;
            OnUnhoverEvent?.Invoke();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsSelectable)
                return;

            OnSelectedEvent?.Invoke(this);
            IsSelectable = false;
        }
    }
}
