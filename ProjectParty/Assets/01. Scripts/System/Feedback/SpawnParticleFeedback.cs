using OMG.Extensions;
using UnityEngine;

namespace OMG.Feedbacks
{
    public class SpawnParticleFeedback : Feedback
    {
        [SerializeField] ParticleSystem particlePrefab = null;
        [SerializeField] float autoDestroyTime = 1f;

        public override void Play(Transform playTrm)
        {
            ParticleSystem instance = Instantiate(particlePrefab);
            instance.transform.position = playTrm.position;
            instance.Play();

            if(autoDestroyTime > 0)
                StartCoroutine(this.DelayCoroutine(autoDestroyTime, () => Destroy(instance)));
        }
    }
}
