using System;
using System.Collections.Generic;
using OMG.Datas;
using UnityEngine;

namespace OMG.Skins
{
    [CreateAssetMenu(menuName = "SO/Skins/SkinLibraray")]
    public class SkinLibrarySO : ScriptableObject
    {
        public string LibraryName = "SKIN";
        [SerializeField] List<SkinSO> skins = null;
        public SkinSO this[int index] => skins[index];
        public int Count => skins.Count;
        
        private SkinData skinData = null;
        public SkinData SkinData => skinData;

        private int currentIndex = 0;
        public int CurrentIndex { 
            get => currentIndex; 
            set {
                SkinData.CurrentIndex = currentIndex = value;
                OnSkinChangedEvent?.Invoke();
            }
        }

        public SkinSO CurrentSkin => this[currentIndex];

        public event Action OnSkinChangedEvent = null;

        public void Init(SkinData skinData)
        {
            this.skinData = skinData;
            currentIndex = skinData.CurrentIndex;
        }

        public SkinSO GetSkin(int index) => skins[index];
    }
}
