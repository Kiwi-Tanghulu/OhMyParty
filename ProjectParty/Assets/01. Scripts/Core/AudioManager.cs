using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace OMG
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioMixer audioMixer;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        public void SetVolume(AudioGroupType groupType, float volume)
        {
            Debug.Log(volume * 100f - 80f);
            audioMixer.SetFloat(groupType.ToString(), volume * 100f - 80f);
        }
    }
}