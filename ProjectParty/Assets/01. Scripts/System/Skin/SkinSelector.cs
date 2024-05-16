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

        // 호스트만 Lobby의 초기화 부분에서 실행해주면 됨
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
