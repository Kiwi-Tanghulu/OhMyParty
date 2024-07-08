using UnityEngine;

namespace OMG.Minigames.Utility
{
    public class SpawnPosition : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        public bool Used { get; private set; } = false;

        public void Active()
        {
            Used = true;
        }

        public void Release()
        {
            Used = false;
        }
    }
}
