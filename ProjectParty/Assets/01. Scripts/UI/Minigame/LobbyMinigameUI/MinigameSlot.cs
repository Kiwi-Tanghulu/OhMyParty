using OMG.Minigames;
using TinyGiantStudio.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace OMG.UI
{
    public class MinigameSlot : UIObject
    {
        private MinigameSO minigameSO;
        public MinigameSO MinigameSO => minigameSO;

        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Image minigameImage;

        [SerializeField] private GameObject showImage;
        [SerializeField] private GameObject hideImage;

        public UnityEvent OnSelectedEvent;
        public UnityEvent OnDeselectedEvent;

        private int showHash = Animator.StringToHash("show");
        private int hideHash = Animator.StringToHash("hide");
        private int showcContentHash = Animator.StringToHash("showContent");
        private int hidecContentHash = Animator.StringToHash("hideContent");
        private Animator anim;

        private void Awake()
        {
            anim = GetComponent<Animator>();
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
            OnSelectedEvent?.Invoke();
        }

        public void Deselected()
        {
            OnDeselectedEvent?.Invoke();
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
    }
}
