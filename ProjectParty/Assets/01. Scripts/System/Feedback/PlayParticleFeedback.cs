using System.Collections.Generic;
using UnityEngine;

namespace OMG.Feedbacks
{
    public class PlayParticleFeedback : Feedback
    {
        private List<ParticleSystem> particles = null;

        private void Awake()
        {
            particles = new List<ParticleSystem>();
            transform.GetComponentsInChildren<ParticleSystem>(particles);
        }

        public override void Play(Vector3 playPos)
        {
            particles.ForEach(i => i.Play());
        }
    }
}
