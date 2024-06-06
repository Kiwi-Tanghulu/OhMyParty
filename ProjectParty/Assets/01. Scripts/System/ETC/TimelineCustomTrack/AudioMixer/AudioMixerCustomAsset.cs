using UnityEngine;
using UnityEngine.Playables;

namespace OMG
{
    public class AudioMixerCustomAsset : PlayableAsset
    {
        public AudioMixerCustomBehaviour template;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<AudioMixerCustomBehaviour>.Create(graph, template);

            return playable;
        }
    }
}