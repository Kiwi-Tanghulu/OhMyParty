using DG.Tweening;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] Transform bladeTransform = null;
        [SerializeField] Vector3 bladeDirection = Vector3.up;
        [SerializeField] float growDuration = 1f;

        private bool activeCollision = false;

        private void OnTriggerEnter(Collider other)
        {
            if(activeCollision == false)
                return;

            if(other.TryGetComponent<IDamageable>(out IDamageable id) == false)
                return;

            id?.OnDamaged(100, transform, other.transform.position);
        }

        public void SetLength(float length)
        {
            bladeTransform.DOKill();
            bladeTransform.DOScale(Vector3.one + bladeDirection * length, growDuration);
        }

        /// <summary>
        /// only owner could call this method
        /// </summary>
        public void ActiveCollision(bool active)
        {
            activeCollision = active;
        }
    }
}
