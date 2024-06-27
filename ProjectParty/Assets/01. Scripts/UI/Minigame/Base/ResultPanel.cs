using UnityEngine;

namespace OMG.UI.Minigames
{
    public class ResultPanel : MonoBehaviour
    {
        [SerializeField] ResultSlot[] slots = null;
        public ResultSlot this[int index] => slots[index];

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
