using TinyGiantStudio.Text;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTileVisual : MonoBehaviour
    {
        private Modular3DText numberText = null;

        private void Awake()
        {
            numberText = transform.Find("NumberText").GetComponent<Modular3DText>();
        }

        public void SetNumberText(int number)
        {
            numberText.Text = number.ToString();
        }
    }
}
