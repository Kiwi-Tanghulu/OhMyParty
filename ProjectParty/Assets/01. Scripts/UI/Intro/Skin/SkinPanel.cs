using OMG.Skins;
using OMG.UI.Solid;
using TinyGiantStudio.Text;
using UnityEngine;

namespace OMG.UI.Skin
{
    public class SkinPanel : MonoBehaviour
    {
        [SerializeField] Modular3DText panelTitle = null;
        [SerializeField] Modular3DText skinTitle = null;

        [SerializeField] SolidButton purchaseButton = null;
        [SerializeField] SolidButton selectButton = null;

        private SkinLibrarySO skinLibrary = null;
        private SkinLibrarySO visualLibrary = null;
        private SkinSelector visualSelector = null;

        private int skinCache = 0;

        private void Awake()
        {
            visualSelector = GetComponent<SkinSelector>();
        }

        private void Start()
        {
            Display(false);
        }

        public void Display(bool active)
        {
            if(skinLibrary)
            {
                if(active)
                {
                    visualSelector.SetSkin();
                    skinCache = skinLibrary.CurrentIndex;
                }
                else
                {
                    visualSelector.ReleaseSkin();
                    skinLibrary.CurrentIndex = skinCache;
                }
            }

            gameObject.SetActive(active);
        }

        public void SetSkinLibrary(SkinLibrarySO skinLibrary)
        {
            this.skinLibrary = skinLibrary;
            panelTitle.Text = skinLibrary.LibraryName;
        }

        public void SetVisualLibrary(SkinLibrarySO visualLibrary)
        {
            visualSelector.ReleaseSkin();
            visualSelector.SetSkinLibrary(visualLibrary);
            this.visualLibrary = visualLibrary;
        }

        public void MoveSkinIndex(int amount)
        {
            int current = visualLibrary.CurrentIndex;
            int limit = visualLibrary.Count;
            visualLibrary.CurrentIndex = (current + amount + limit) % limit;

            visualSelector.SetSkin();
        }

        public void SelectSkin()
        {
            skinCache = visualLibrary.CurrentIndex;
        }

        public void PurchaseSkin()
        {
            // process purchase
            SelectSkin();
        }
    }
}
