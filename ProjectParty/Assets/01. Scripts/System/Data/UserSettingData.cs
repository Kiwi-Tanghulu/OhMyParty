using System.Collections.Generic;

namespace OMG.Datas
{
    [System.Serializable]
    public class UserSettingData 
    {
        public Dictionary<string, float> VolumeMap = null;
        public string Nickname = "Player";

        public UserSettingData()
        {
            VolumeMap = new Dictionary<string, float>() {
                ["Master"] = 0.5f,
                ["VFX"] = 0.5f,
                ["BGM"] = 0.5f
            };
            Nickname = null;
        }
    }
}
