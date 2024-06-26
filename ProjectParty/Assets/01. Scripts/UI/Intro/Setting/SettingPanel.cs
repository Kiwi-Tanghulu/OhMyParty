using OMG.Datas;
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

        public void DisplayCredit(bool active)
        {
            Debug.Log($"Display Credit : {active}");
        }

        public void ResetData()
        {
            DataManager.ClearData();
            DataManager.LoadData();
        }
    }
}
