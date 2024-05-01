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
        [SerializeField] private Image keyImagePrefab;
        [SerializeField] private TextMeshProUGUI keyNameText;

        public void DisplayKeyInfo(ControlKeyInfo keyInfo)
        {
            foreach(Sprite controlKeyImage in keyInfo.ControlKeyImages)
            {
                Image keyImage = Instantiate(keyImagePrefab, transform);
                keyImage.sprite = controlKeyImage;
            }

            keyNameText.text = keyInfo.ControlKeyName;
            keyNameText.transform.SetAsLastSibling();
        }
    }
}
