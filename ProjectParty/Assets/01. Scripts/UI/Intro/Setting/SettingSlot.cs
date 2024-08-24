using OMG.Datas;
using UnityEngine;

namespace OMG.UI.Settings
{
    public abstract class SettingSlot : MonoBehaviour
    {
        protected UserSettingData settingData = null;
        
        protected virtual void Awake()
        {
            settingData = DataManager.UserData.SettingData;
        }

        public virtual void Init() {}
    }
}
