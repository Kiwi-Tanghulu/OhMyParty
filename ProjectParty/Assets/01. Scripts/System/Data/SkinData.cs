using System.Collections.Generic;

namespace OMG.Datas
{
    [System.Serializable]
    public class SkinData
    {
        public HashSet<int> UnlockedSkin = null;
        public int CurrentIndex = 0;

        public SkinData()
        {
            UnlockedSkin = new HashSet<int>();
            CurrentIndex = 0;
        }
    }
}
