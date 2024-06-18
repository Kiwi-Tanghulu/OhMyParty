using OMG.Skins;
using UnityEngine;

namespace OMG.Datas
{
    public class DataLoader : MonoBehaviour
    {
        [Header("Skin Data")]
        [SerializeField] SkinLibrarySO characterSkinLibrary = null;
        [SerializeField] SkinLibrarySO lobbySkinLibrary = null;
    
        #if UNITY_EDITOR
        public UserData UserData = null;
        #endif

        private void Awake()
        {
            DataManager.LoadData();

            characterSkinLibrary.Init(DataManager.UserData.SkinData.CharacterSkin);
            lobbySkinLibrary.Init(DataManager.UserData.SkinData.LobbySkin);
            
            #if UNITY_EDITOR
            UserData = DataManager.UserData;
            #endif
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        ClearData();
                    }
                }
            }
        }

        private void OnDestroy()
        {
            DataManager.SaveData();    
        }

        [ContextMenu("ClearData")]
        public void ClearData()
        {
            DataManager.ClearData();
            DataManager.LoadData();
        }
    }
}
