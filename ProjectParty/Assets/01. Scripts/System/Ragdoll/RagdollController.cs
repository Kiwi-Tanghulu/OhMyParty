using OMG.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Ragdoll
{
    public class RagdollController : MonoBehaviour
    {
        [SerializeField] private Transform copyTargetRoot;

        [Space]
        [SerializeField] private Rigidbody hipRb;
        public Rigidbody HipRb => hipRb;

        [Space]
        [SerializeField] protected bool onInitActive;

        [Space]
        public UnityEvent OnActiveEvent;
        public UnityEvent OnDeactiveEvent;

        protected RagdollPart[] parts;

        protected virtual void Awake()
        {
            parts = GetComponentsInChildren<RagdollPart>();
            for (int i = 0; i < parts.Length; i++)
                parts[i].Init(copyTargetRoot.FindFromAll(parts[i].gameObject.name));

            gameObject.SetActive(onInitActive);
        }

        public virtual void SetActive(bool value)
        {
            gameObject.SetActive(value);

            if (value)
            {
                for (int i = 0; i < parts.Length; i++)
                    parts[i].Copy();

                OnActiveEvent?.Invoke();
            }
            else
            {
                OnDeactiveEvent?.Invoke();
            }
        }

        public void AddForce(Vector3 power, ForceMode mode)
        {
            hipRb.AddForce(power, mode);
        }
    }
}