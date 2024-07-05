using DG.Tweening;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class Sword : MonoBehaviour
    {
        [SerializeField] Transform bladeTransform = null;
        [SerializeField] Vector3 bladeDirection = Vector3.up;
        [SerializeField] float growDuration = 1f;

        public void SetLength(float length)
        {
            bladeTransform.DOKill();
            bladeTransform.DOScale(bladeDirection * length, growDuration);
        }
    }
}
