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

        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Image minigameImage;

        public UnityEvent OnSelectedEvent;
        public UnityEvent OnDeselectedEvent;

        public void SetMinigameSO(MinigameSO minigameSO)
        {
            this.minigameSO = minigameSO;

            titleText.text = minigameSO.MinigameName;
            //minigameImage.sprite = minigameSO.MinigameImage;
        }

        public void Selected()
        {
            OnSelectedEvent?.Invoke();
        }

        public void Deselected()
        {
            OnDeselectedEvent?.Invoke();
        }
    }
}
