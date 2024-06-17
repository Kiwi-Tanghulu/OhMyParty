using UnityEngine;

namespace OMG.Skins
{
    public class CharacterSkin : Skin
    {
        private Material mat;
        public Material Mat => mat;

        [SerializeField] private SkinnedMeshRenderer skinnedMesh;
        public SkinnedMeshRenderer SkinnedMesh => skinnedMesh;

        public override void Init()
        {
            base.Init();

            gameObject.name = "Skin";

            mat = new Material(skinnedMesh.sharedMaterial);
            skinnedMesh.material = mat;
        }

        public override void Release()
        {
            base.Release();
        }
    }
}
