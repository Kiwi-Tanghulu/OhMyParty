using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI
{
    public class PlayerReadyCheckBox : CheckBox
    {
        [SerializeField] private GameObject fillImage;

        public override void SetCheck(bool value)
        {
            fillImage.SetActive(value);
        }
    }
}