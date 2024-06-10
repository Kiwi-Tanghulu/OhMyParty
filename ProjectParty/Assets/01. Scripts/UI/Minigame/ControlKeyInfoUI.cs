using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class ControlKeyInfoUI : MonoBehaviour
    {
        [SerializeField] private ControlKeyUI keyImagePrefab;
        [SerializeField] private TextMeshProUGUI keyNameText;

        public void DisplayKeyInfo(ControlKeyInfo keyInfo)
        {
            foreach(ControlKey controlKey in keyInfo.ControlKeys)
            {
                ControlKeyUI keyImage = Instantiate(keyImagePrefab, transform);
                keyImage.SetControlKey(controlKey);
            }

            keyNameText.text = keyInfo.ControlKeyName;
            keyNameText.transform.SetAsLastSibling();
        }
    }
}
