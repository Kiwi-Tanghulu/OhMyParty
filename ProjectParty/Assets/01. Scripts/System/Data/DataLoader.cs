using System.Collections.Generic;
using OMG.Skins;
using UnityEngine;
using UnityEngine.Audio;

namespace OMG.Datas
{
    public class DataLoader : MonoBehaviour
    {
        [Header("Skin Data")]
        [SerializeField] SkinLibrarySO characterSkinLibrary = null;
        [SerializeField] SkinLibrarySO lobbySkinLibrary = null;

        [Header("Audio")]
        [SerializeField] AudioMixer audioMixer = null;
    
        #if UNITY_EDITOR
        public UserData UserData = null;
        #endif

        public void LoadData()
        {
            DataManager.LoadData();

            characterSkinLibrary.Init(DataManager.UserData.SkinData.CharacterSkin);
            lobbySkinLibrary.Init(DataManager.UserData.SkinData.LobbySkin);

            Dictionary<string, float> volumeMap = DataManager.UserData.SettingData.VolumeMap;
            foreach(string volumeKey in volumeMap.Keys)
            {
                audioMixer.SetFloat(volumeKey, volumeMap[volumeKey]);
                Debug.Log(volumeMap[volumeKey]);
            }
            
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
