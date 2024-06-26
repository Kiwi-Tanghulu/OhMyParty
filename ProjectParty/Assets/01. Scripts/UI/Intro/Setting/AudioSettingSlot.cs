using UnityEngine;
using UnityEngine.Audio;

namespace OMG.UI.Settings
{
    public class AudioSettingSlot : MonoBehaviour
    {
        [SerializeField] AudioMixer audioMixer = null;
        [SerializeField] string groupKey = "";

        private const float MAX_VOLUME = 0f;
        private const float MIN_VOLUME = -40f;
        private const float MUTE_VOLUME = -80f;

        public void HandleValueChanged(float value)
        {
            float volume;
            if(value <= 0.01f)
                volume = MUTE_VOLUME;
            else
                volume = Mathf.Lerp(MIN_VOLUME, MAX_VOLUME, value);

            audioMixer.SetFloat(groupKey, volume);
        }
    }
}
