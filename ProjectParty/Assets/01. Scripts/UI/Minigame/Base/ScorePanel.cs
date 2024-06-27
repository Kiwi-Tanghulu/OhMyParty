using UnityEngine;

namespace OMG.UI.Minigames
{
    public class ScorePanel : MonoBehaviour
    {
        [SerializeField] ScoreSlot[] slots = null;
        public ScoreSlot this[int index] => slots[index];

        private void Start()
        {
            Display(false);
        }

        public void Display(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
