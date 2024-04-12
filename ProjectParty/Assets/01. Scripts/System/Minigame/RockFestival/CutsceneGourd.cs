using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class CutsceneGourd : MonoBehaviour
    {
        [SerializeField] ParticleSystem particle = null;

        public void OnGourdBroken()
        {
            particle.Play();
        }
    }
}
