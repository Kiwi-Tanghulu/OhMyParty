using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace OMG
{
    [System.Serializable]
    public class AudioMixerCustomBehaviour : PlayableBehaviour
    {
        public AudioGroupType groupType;
        public float volume;
    }
}