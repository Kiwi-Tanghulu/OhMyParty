using System.Collections.Generic;

namespace OMG.Datas
{
    [System.Serializable]
    public class SkinData
    {
        public List<int> UnlockedSkin = null;
        public int CurrentIndex = 0;

        public bool IsUnlocked(int index) => UnlockedSkin.Contains(index);

        public SkinData()
        {
            UnlockedSkin = new List<int>() { 0 };
            CurrentIndex = 0;
        }
    }
}
