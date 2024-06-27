using TinyGiantStudio.Text;
using TMPro;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class ResultSlot : MonoBehaviour
    {
        [SerializeField] TMP_Text nameText = null;
        [SerializeField] TMP_Text scoreText = null;

        public const string NAME_FORMAT = "{0} :";

        private void Awake()
        {
            SetResult(null, -1);
        }

        public void SetResult(string name, int score)
        {
            if(string.IsNullOrEmpty(name))
            {
                nameText.text = " -";
                scoreText.text = "-";
                return;
            }

            nameText.text = string.Format(NAME_FORMAT, name);
            scoreText.text = score.ToString();
        }
    }
}
