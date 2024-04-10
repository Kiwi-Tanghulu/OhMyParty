using TinyGiantStudio.Text;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class ResultSlot : MonoBehaviour
    {
        [SerializeField] Modular3DText nameText = null;
        [SerializeField] Modular3DText scoreText = null;

        public const string NAME_FORMAT = "{0} :";

        private void Awake()
        {
            SetResult(null, -1);
        }

        public void SetResult(string name, int score)
        {
            if(string.IsNullOrEmpty(name))
            {
                nameText.Text = " -";
                scoreText.Text = "-";
                return;
            }

            nameText.Text = string.Format(NAME_FORMAT, name);
            scoreText.Text = score.ToString();
        }
    }
}
