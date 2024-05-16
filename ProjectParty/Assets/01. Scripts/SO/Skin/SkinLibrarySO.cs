using System;
using System.Collections.Generic;
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

        private int currentIndex = 0;
        public int CurrentIndex { 
            get => currentIndex; 
            set {
                currentIndex = value;
                OnSkinChangedEvent?.Invoke();
            }
        }

        public SkinSO CurrentSkinData => this[currentIndex];

        public event Action OnSkinChangedEvent = null;
    }
}
