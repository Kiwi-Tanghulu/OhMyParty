using UnityEngine;

namespace OMG.Skins
{
    public class SkinSelector : MonoBehaviour
    {
        [SerializeField] SkinLibrarySO skinLibrary = null;
        [SerializeField] Transform skinContainer = null;

        private Skin currentSkin = null;

        private void Awake()
        {
            skinLibrary.OnSkinChangedEvent += SetSkin;
        }

        private void OnDestroy()
        {
            skinLibrary.OnSkinChangedEvent -= SetSkin;
        }

        public void SetSkin()
        {
            if(currentSkin != null)
                ReleaseSkin();

            currentSkin = Instantiate(skinLibrary.CurrentSkinData.SkinPrefab, skinContainer);
            currentSkin.Init();
        }

        public void ReleaseSkin()
        {
            currentSkin.Release();
            Destroy(currentSkin.gameObject);

            currentSkin = null;
        }
    }
}
