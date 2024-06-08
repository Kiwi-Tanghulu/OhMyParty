using OMG.Audio;
using UnityEngine;

namespace OMG.Feedbacks
{
    public class PlayAudioFeedback : Feedback
    {
        private AudioSource player;
        [SerializeField] private AudioClip clip;

        private void Awake()
        {
            player = GetComponent<AudioSource>();
            if (player == null)
                player = DEFINE.GlobalAudioPlayer;
        }

        public override void Play(Vector3 playPos)
        {
            player?.PlayOneShot(clip);
        }
    }
}