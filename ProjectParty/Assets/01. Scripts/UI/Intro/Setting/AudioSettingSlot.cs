using TinyGiantStudio.Text;
using UnityEngine;
using UnityEngine.Audio;

namespace OMG.UI.Settings
{
    public class AudioSettingSlot : SettingSlot
    {
        [SerializeField] AudioMixer audioMixer = null;
        [SerializeField] Slider slider = null;
        [SerializeField] string groupKey = "";

        private const float MAX_VOLUME = 0f;
        private const float MIN_VOLUME = -20f;
        private const float MUTE_VOLUME = -80f;

        public override void Init()
        {
            base.Init();

            float volume = settingData.VolumeMap[groupKey];
            float value = 0;
            if(volume == MUTE_VOLUME)
                value = 0;
            else
                value = (volume - MIN_VOLUME) / (MAX_VOLUME - MIN_VOLUME);

            slider.CurrentValue = value;
        }

        public void HandleValueChanged(float value)
        {
            float volume;
            if(value <= 0.01f)
                volume = MUTE_VOLUME;
            else
                volume = Mathf.Lerp(MIN_VOLUME, MAX_VOLUME, value);

            audioMixer.SetFloat(groupKey, volume);
            settingData.VolumeMap[groupKey] = volume;
        }
    }
}
