using OMG.Skins;
using OMG.UI.Solid;
using UnityEngine;

namespace OMG.UI.Skin
{
    public class SkinPanel : MonoBehaviour
    {
        [SerializeField] SolidButton purchaseButton;
        [SerializeField] SolidButton selectButton;

        private SkinLibrarySO skinLibrary = null;
        private SkinSelector skinSelector = null;

        private int skinCache = 0;

        private void Awake()
        {
            skinSelector = GetComponent<SkinSelector>();
        }

        private void Start()
        {
            Display(false);
        }

        public void Display(bool active)
        {
            if(active)
            {
                skinSelector.SetSkin();
                skinCache = skinLibrary.CurrentIndex;
            }
            else
            {
                skinSelector.ReleaseSkin();
                skinLibrary.CurrentIndex = skinCache;
            }

            gameObject.SetActive(active);
        }

        public void SetSkinLibrary(SkinLibrarySO library)
        {
            skinSelector.ReleaseSkin();
            skinSelector.SetSkinLibrary(library);
            skinLibrary = library;
        }

        public void MoveSkinIndex(int amount)
        {
            int current = skinLibrary.CurrentIndex;
            int limit = skinLibrary.Count;
            skinLibrary.CurrentIndex = (current + amount + limit) % limit;

            skinSelector.SetSkin();
        }

        public void SelectSkin()
        {
            skinCache = skinLibrary.CurrentIndex;
        }

        public void PurchaseSkin()
        {
            // process purchase
            SelectSkin();
        }
    }
}
