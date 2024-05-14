using System;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Skins
{
    [CreateAssetMenu(menuName = "SO/Skins/SkinLibraray")]
    public class SkinLibrarySO : ScriptableObject
    {
        [SerializeField] List<SkinSO> skins = null;
        public SkinSO this[int index] => skins[index];

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
