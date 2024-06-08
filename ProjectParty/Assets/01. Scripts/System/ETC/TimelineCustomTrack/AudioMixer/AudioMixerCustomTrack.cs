using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace OMG
{
    [TrackClipType(typeof(AudioMixerCustomAsset))]
    [TrackBindingType(typeof(AudioMixer))]
    public class AudioMixerCustomTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            return ScriptPlayable<AudioMixerCustomMixerBehaviour>.Create(graph, inputCount);
        }
    }
}
