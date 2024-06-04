using OMG.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Feedbacks
{
    public class PlayAudioFeedback : Feedback
    {
        private AudioSource player;
        [SerializeField] private AudioLibrarySO audioLib;

        private void Awake()
        {
            player = GetComponent<AudioSource>();
            if (player == null)
                player = DEFINE.GlobalAudioPlayer;
        }

        public override void Play(Transform playTrm)
        {
            player?.PlayOneShot(GetAudioClip());
        }

        private AudioClip GetAudioClip()
        {
            return audioLib.GetRandomAudio().Audio;
        }
    }
}