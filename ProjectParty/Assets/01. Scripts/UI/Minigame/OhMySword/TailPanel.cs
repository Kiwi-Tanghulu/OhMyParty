using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI.Minigames.OhMySword
{
    public class TailPanel : MonoBehaviour
    {
        private Image tailIcon = null;

        private void Awake()
        {
            tailIcon = GetComponent<Image>();
        }

        public void SetColor(Color color)
        {
            tailIcon.color = color;
        }
    }
}
