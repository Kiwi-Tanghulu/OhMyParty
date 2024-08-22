using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI
{
    public class PlayerReadyCheckBox : CheckBox
    {
        [SerializeField] private RawImage playerImage;
        [SerializeField] private PlayerNameTag nameTag;

        public void SetPlayerImage(Texture texture)
        {
            playerImage.texture = texture;
        }

        public void SetNameText(string name)
        {
            nameTag.SetNameTag(name);
        }
    }
}