using UnityEngine;
using OMG.Editor;

namespace OMG.Feedbacks
{
    public class SpawnParticleFeedback : Feedback
    {
        [SerializeField] ParticleSystem particlePrefab = null;

        [SerializeField] bool multipleParticle = false;

        [ConditionalField("multipleParticle", true)]
        [SerializeField] MultipleParticlePreset multipleSetting = new MultipleParticlePreset();

        [System.Serializable]
        public class MultipleParticlePreset
        {
            public float Radius = 10f;
            public int Count = 10;

            [Header("Ignore Option")]
            public bool X;
            public bool Y;
            public bool Z;
        }

        public override void Play(Transform playTrm)
        {
            if(multipleParticle)
                SpawnMultipleParticle(playTrm);            
            else
                SpawnParticle(playTrm.position);
        }

        private void SpawnParticle(Vector3 position)
        {
            ParticleSystem instance = Instantiate(particlePrefab);
            instance.transform.position = position;
            instance.Play();
        }

        private void SpawnMultipleParticle(Transform playTrm)
        {
            for(int i = 0; i < multipleSetting.Count; ++i)
            {
                Vector3 randomPosition = Random.insideUnitSphere * multipleSetting.Radius;
                if (multipleSetting.X)
                    randomPosition.x = 0f;
                if (multipleSetting.Y)
                    randomPosition.y = 0f;
                if (multipleSetting.Z)
                    randomPosition.z = 0f;

                Vector3 position = playTrm.position + randomPosition;
                SpawnParticle(position);
            }
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(multipleParticle == false)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, multipleSetting.Radius);
        }
        #endif
    }
}
