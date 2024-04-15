using TinyGiantStudio.Text;
using Unity.Netcode;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTileVisual : NetworkBehaviour
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
