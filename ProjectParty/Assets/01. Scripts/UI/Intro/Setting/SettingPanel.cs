using UnityEngine;

namespace OMG.UI.Settings
{
    public class SettingPanel : MonoBehaviour
    {
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
