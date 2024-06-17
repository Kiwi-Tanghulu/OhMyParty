using UnityEngine;

namespace OMG.Skins
{
    public class CharacterSkin : Skin
    {
        public override void Init()
        {
            base.Init();
            gameObject.name = "Skin";
        }

        public override void Release()
        {
            base.Release();
        }
    }
}
