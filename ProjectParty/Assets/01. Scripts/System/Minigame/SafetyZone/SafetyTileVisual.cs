using TMPro;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTileVisual : MonoBehaviour
    {
        private TMP_Text numberText = null;

        private void Awake()
        {
            numberText = transform.Find("NumberText").GetComponent<TMP_Text>();
        }

        public void SetNumberText(int number)
        {
            if(number == -1)
                numberText.text = "-";
            else
                numberText.text = number.ToString();
        }
    }
}
