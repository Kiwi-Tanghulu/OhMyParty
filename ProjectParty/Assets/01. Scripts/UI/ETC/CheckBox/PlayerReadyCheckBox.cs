using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class PlayerReadyCheckBox : CheckBox
    {
        [SerializeField] private GameObject fillImage;
        [SerializeField] private RawImage playerImage;

        public void SetPlayerImage(Texture texture)
        {
            playerImage.texture = texture;
        }

        public override void SetCheck(bool value)
        {
            fillImage.SetActive(value);
        }
    }
}