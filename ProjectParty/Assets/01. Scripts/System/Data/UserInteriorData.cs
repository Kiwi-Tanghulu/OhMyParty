using System.Collections.Generic;
using OMG.Interiors;

namespace OMG.Datas
{
    [System.Serializable]
    public class UserInteriorData 
    {
        public List<InteriorData> InteriorPrests = null;
        public int PresetCount = 3;
        public int CurrentPreset = 0;

        public UserInteriorData()
        {
            PresetCount = 3;
            InteriorPrests = new List<InteriorData>(new InteriorData[PresetCount]);
        }
    }
}
