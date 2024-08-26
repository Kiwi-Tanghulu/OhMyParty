using OMG.Minigames;
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

        [Space]
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Image minigameImage;

        [Space]
        [SerializeField] private Image frameImage;
        [SerializeField] private Color hoverColor;
        [SerializeField] private Color unhoverColor;

        [Space]
        public UnityEvent OnHoverEvent;
        public UnityEvent OnUnhoverEvent;

        public bool IsSelectable;

        public override void Init()
        {
            base.Init();

            frameImage.color = unhoverColor;
        }

        public void SetMinigameSO(MinigameSO minigameSO)
        {
            this.minigameSO = minigameSO;

            titleText.text = minigameSO.MinigameName;
            if(minigameSO.MinigameImage != null)
                minigameImage.sprite = minigameSO.MinigameImage;
        }

        public void Hover()
        {
            frameImage.color = hoverColor;
            OnHoverEvent?.Invoke();
        }

        public void Unhover()
        {
            frameImage.color = unhoverColor;
            OnUnhoverEvent?.Invoke();
        }
    }
}
