using System.Collections.Generic;
using OMG.Datas;
using UnityEngine;

namespace OMG.UI.Settings
{
    public class SettingPanel : MonoBehaviour
    {
        [SerializeField] List<AudioSettingSlot> audioSlots = new List<AudioSettingSlot>();

        private void Start()
        {
            Display(false);
        }

        public void Display(bool active)
        {
            if(active)
                audioSlots.ForEach(i => i.Init());

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

        private void LoadData()
        {

        }
    }
}
