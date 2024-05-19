using UnityEngine;

namespace OMG.Skins
{
    public class SkinSelector : MonoBehaviour
    {
        [SerializeField] Transform skinContainer = null;
        [SerializeField] SkinLibrarySO skinLibrary = null;
        public SkinLibrarySO SkinLibrary => skinLibrary;

        private Skin currentSkin = null;
        public Skin CurrentSkin => currentSkin;

        public void SetSkinLibrary(SkinLibrarySO library)
        {
            skinLibrary = library;
        }

        // 호스트만 Lobby의 초기화 부분에서 실행해주면 됨
        public void SetSkin() 
        {
            if(currentSkin != null)
                ReleaseSkin();

            if(skinLibrary.CurrentSkinData.SkinPrefab != null)
            {
                currentSkin = Instantiate(skinLibrary.CurrentSkinData.SkinPrefab, skinContainer);
                currentSkin.Init();
            }
        }

        public void ReleaseSkin()
        {
            if(currentSkin != null)
            {
                currentSkin.Release();
                Destroy(currentSkin.gameObject);
            }

            currentSkin = null;
        }
    }
}
