using OMG.Minigames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class MinigameSlot : UIObject
    {
        private MinigameSO minigameSO;
        public MinigameSO MinigameSO => minigameSO;

        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private Image minigameImage;

        public void SetMinigameSO(MinigameSO minigameSO)
        {
            this.minigameSO = minigameSO;

            titleText.text = minigameSO.MinigameName;
            //minigameImage.sprite = minigameSO.MinigameImage;
        }
    }
}
