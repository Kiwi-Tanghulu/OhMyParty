using OMG.Extensions;
using UnityEngine;

namespace OMG.Feedbacks
{
    public class SpawnParticleFeedback : Feedback
    {
        [SerializeField] ParticleSystem particlePrefab = null;
        [SerializeField] float autoDestroyTime = 1f;
        private ParticleSystem instance = null;

        public override void Play(Transform playTrm)
        {
            if(instance != null)
            {
                StopAllCoroutines();
                DestroyInstance();
            }

            instance = Instantiate(particlePrefab);
            instance.transform.position = playTrm.position;
            instance.Play();

            if(autoDestroyTime > 0)
                StartCoroutine(this.DelayCoroutine(autoDestroyTime, DestroyInstance));
        }

        private void DestroyInstance()
        {
            if(instance == null)
                return;

            Destroy(instance.gameObject);
            instance = null;
        }
    }
}
