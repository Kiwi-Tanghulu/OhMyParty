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
        [SerializeField] private bool onInitActive;

        [Space]
        public UnityEvent OnActiveEvent;
        public UnityEvent OnDeactiveEvent;

        private RagdollPart[] parts;

        private void Awake()
        {
            parts = GetComponentsInChildren<RagdollPart>();
            for (int i = 0; i < parts.Length; i++)
                parts[i].Init(copyTargetRoot.FindFromAll(parts[i].gameObject.name));

            gameObject.SetActive(onInitActive);
        }

        public void SetActive(bool value)
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

        //private List<RagdollCopyPart> rp;
        //private List<AnimationCopyPart> ap;
        //private List<TestCopyMotion> cp;

        //public Transform RagdollCopyTargetRoot;
        //public Transform AnimCopyTargetRoot;

        //[Range(0f, 1f)]
        //[SerializeField] private float ragdollWeight;

        //private void Awake()
        //{
        //    rp = RagdollCopyTargetRoot.GetComponentsInChildren<RagdollCopyPart>().ToList();
        //    ap = AnimCopyTargetRoot.GetComponentsInChildren<AnimationCopyPart>().ToList();
        //    cp = GetComponentsInChildren<TestCopyMotion>().ToList();

        //    foreach (RagdollCopyPart ragdoll in rp)
        //        ragdoll.Init(AnimCopyTargetRoot, RagdollCopyTargetRoot);
        //    foreach (AnimationCopyPart anim in ap)
        //        anim.Init(AnimCopyTargetRoot, RagdollCopyTargetRoot);
        //    foreach (TestCopyMotion c in cp)
        //        c.Init(rp.Find(x => x.name == c.name), ap.Find(x => x.name == c.name), ragdollWeight);
        //}

        //private void Update()
        //{
        //    for(int i = 0; i < cp.Count; i++)
        //        cp[i].SetRagdollWeight(ragdollWeight);
        //}
    }
}