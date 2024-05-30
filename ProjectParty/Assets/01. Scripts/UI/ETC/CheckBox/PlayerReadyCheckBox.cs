using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class PlayerReadyCheckBox : CheckBox
    {
        [SerializeField] private GameObject fillImage;
        [SerializeField] private Image playerImage;

        public void SetPlayerImage(Sprite sprite)
        {
            playerImage.sprite = sprite;
        }

        public override void SetCheck(bool value)
        {
            fillImage.SetActive(value);
        }
    }
}