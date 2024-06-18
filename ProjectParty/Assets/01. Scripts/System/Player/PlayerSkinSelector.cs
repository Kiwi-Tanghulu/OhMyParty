using OMG.Skins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerSkinSelector : SkinSelector
    {
        [SerializeField] private SkinnedMeshRenderer refRenderer;

        [SerializeField] private bool useOutline;

        public override void SetSkin(SkinSO skin)
        {
            base.SetSkin(skin);

            Debug.Log("Error : PlayerSkinSelector");

            if(CurrentSkin != null)
            {
                (CurrentSkin as CharacterSkin).SkinnedMesh.bones = refRenderer.bones;

                if (useOutline)
                    CurrentSkin.gameObject.AddComponent<PlayerOutLine>();
            }
        }
    }
}