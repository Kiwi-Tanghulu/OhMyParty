using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace OMG
{
    public class AudioMixerCustomMixerBehaviour : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            AudioMixer trackBinding = playerData as AudioMixer;
            float finalValue = 0f;
            AudioGroupType groupType = AudioGroupType.Master; 

            if (!trackBinding)
                return;

            int inputCount = playable.GetInputCount(); //get the number of all clips on this track

            for (int i = 0; i < inputCount; i++)
            {
                float inputWeight = playable.GetInputWeight(i);
                ScriptPlayable<AudioMixerCustomBehaviour> inputPlayable = (ScriptPlayable<AudioMixerCustomBehaviour>)playable.GetInput(i);
                AudioMixerCustomBehaviour input = inputPlayable.GetBehaviour();

                // Use the above variables to process each frame of this playable.
                finalValue += input.volume * inputWeight;
                
                groupType = input.groupType;
            }

            //assign the result to the bound object
            trackBinding.SetFloat(groupType.ToString(), finalValue * 100f - 80f);
        }
    }
}