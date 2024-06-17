using OMG.Skins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerSkinSelector : SkinSelector
    {
        [SerializeField] private bool useOutline;

        public override void SetSkin(SkinSO skin)
        {
            base.SetSkin(skin);

            if(CurrentSkin != null)
            {
                if (useOutline)
                    CurrentSkin.gameObject.AddComponent<PlayerOutLine>();
            }
        }
    }
}