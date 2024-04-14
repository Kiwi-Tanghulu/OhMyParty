using UnityEngine;

namespace OMG.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField] protected AudioLibrarySO audioLibrary = null;
        protected AudioSource player = null;

        private void Awake()
        {
            player = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (player == null)
                player = DEFINE.GlobalAudioPlayer;
        }

        public void PlayAudio(string key)
        {
            Stop();
            player.clip = audioLibrary[key];
            player?.Play();
        }

        public void PlayOneShot(string key)
        {
            player?.PlayOneShot(audioLibrary[key]);
        }

        public void Pause()
        {
            player.Pause();
        }

        public void Stop()
        {
            player.Stop();
        }
    }
}