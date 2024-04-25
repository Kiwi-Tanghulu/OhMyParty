using System.Collections.Generic;
using OMG.Interiors;

namespace OMG.Datas
{
    [System.Serializable]
    public class UserData
    {
        public List<InteriorData> InteriorPrests = null;

        public void CreateData()
        {
            InteriorPrests = new List<InteriorData>();
        }
    }
}
