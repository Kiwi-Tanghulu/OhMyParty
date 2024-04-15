using UnityEngine;

namespace OMG.Feedbacks
{
    public class SpawnParticleFeedback : Feedback
    {
        [SerializeField] ParticleSystem particlePrefab = null;

        public override void Play(Transform playTrm)
        {
            ParticleSystem instance = Instantiate(particlePrefab);
            instance.transform.position = playTrm.position;
            instance.Play();
        }
    }
}
