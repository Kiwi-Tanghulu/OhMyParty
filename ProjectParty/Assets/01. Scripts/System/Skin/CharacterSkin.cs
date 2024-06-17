using UnityEngine;

namespace OMG.Skins
{
    public class CharacterSkin : Skin
    {
        private Material mat;
        public Material Mat => mat;

        private SkinnedMeshRenderer skinnedMesh;
        public SkinnedMeshRenderer SkinnedMesh => skinnedMesh;

        public override void Init()
        {
            base.Init();

            gameObject.name = "Skin";

            Renderer render = GetComponent<Renderer>();
            mat = new Material(render.sharedMaterial);
            render.material = mat;

            skinnedMesh = GetComponent<SkinnedMeshRenderer>();
        }

        public override void Release()
        {
            base.Release();
        }
    }
}
