using OMG.Audio;
using UnityEngine;

namespace OMG.Feedbacks
{
    public class PlayRandomAudioFeedback : Feedback
    {
        private AudioSource player;
        [SerializeField] private AudioLibrarySO audioLib;

        private void Awake()
        {
            player = GetComponent<AudioSource>();
            if (player == null)
                player = DEFINE.GlobalAudioPlayer;
        }

        public override void Play(Vector3 playPos)
        {
            player?.PlayOneShot(GetAudioClip());
        }

        private AudioClip GetAudioClip()
        {
            return audioLib.GetRandomAudio().Audio;
        }
    }
}