using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class PlayerReadyCheckBox : CheckBox
    {
        [SerializeField] private GameObject fillImage;
        [SerializeField] private RawImage playerImage;
        [SerializeField] private TMP_Text nameText;

        public void SetPlayerImage(Texture texture)
        {
            playerImage.texture = texture;
        }

        public void SetNameText(string name)
        {
            nameText.text = name;
        }

        public override void SetCheck(bool value)
        {
            base.SetCheck(value);

            fillImage.SetActive(value);
        }
    }
}