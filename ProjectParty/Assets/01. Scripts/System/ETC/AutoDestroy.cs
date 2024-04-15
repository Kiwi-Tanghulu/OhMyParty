using OMG.Extensions;
using UnityEngine;

namespace OMG.ETC
{
    public class AutoDestroy : MonoBehaviour
    {
        [SerializeField] float autoDestroyTime = 1f;

        private void Start()
        {
            StartCoroutine(this.DelayCoroutine(autoDestroyTime, () => {
                if(gameObject != null)
                    Destroy(gameObject);
            }));
        }
    }
}
