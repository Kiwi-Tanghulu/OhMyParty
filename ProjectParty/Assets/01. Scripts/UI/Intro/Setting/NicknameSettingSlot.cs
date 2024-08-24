using OMG.Networks;
using TMPro;
using UnityEngine;

namespace OMG.UI.Settings
{
    public class NicknameSettingSlot : SettingSlot
    {
        [SerializeField] TMP_InputField inputField = null;
        [SerializeField] int lengthLimit = 8;

        public override void Init()
        {
            base.Init();

            inputField.text = settingData.Nickname;
        }
        
        public void HandleEndEdit(string text)
        {
            if(string.IsNullOrEmpty(text))
            {
                inputField.text = settingData.Nickname;
                return;
            }

            settingData.Nickname = text;
            ClientManager.Instance.SetNickname(text);
        }

        public void HandleValueChanged(string text)
        {
            if(text.Length > lengthLimit)
                inputField.text = text.Substring(0, lengthLimit);
        }
    }
}
