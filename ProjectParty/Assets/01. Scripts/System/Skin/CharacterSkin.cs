using UnityEngine;

namespace OMG.Skins
{
    public class CharacterSkin : Skin
    {
        private Material mat;
        public Material Mat => mat;

        public override void Init()
        {
            base.Init();

            gameObject.name = "Skin";

            Renderer render = GetComponent<Renderer>();
            mat = new Material(render.sharedMaterial);
            render.material = mat;
        }

        public override void Release()
        {
            base.Release();
        }
    }
}
