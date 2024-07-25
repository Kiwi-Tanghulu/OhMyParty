using OMG.Extensions;
using OMG.NetworkEvents;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Ragdoll
{
    public class RagdollController : CharacterComponent
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

        private NetworkEvent<BoolParams> setActiveRagdollRpc = new NetworkEvent<BoolParams>("SetActiveRagdollRpc");

        protected RagdollPart[] parts;

        public override void Init(CharacterController controller)
        {
            base.Init(controller);

            setActiveRagdollRpc.Register(controller.NetworkObject);
            setActiveRagdollRpc.AddListener(SetActive);

            parts = GetComponentsInChildren<RagdollPart>();
            for (int i = 0; i < parts.Length; i++)
                parts[i].Init(copyTargetRoot.FindFromAll(parts[i].gameObject.name));

            gameObject.SetActive(onInitActive);
        }

        public void SetActive(bool value)
        {
            if (!Controller.IsOwner)
                return;

#if UNITY_EDITOR
            if(Controller.UseInNetwork)
                setActiveRagdollRpc.Broadcast(value);
            else
                setActiveRagdollRpc.Invoke(value);
#else
            setActiveRagdollRpc.Broadcast(value);
#endif
        }

        protected virtual void SetActive(BoolParams value)
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

        public void AddForce(float power, Vector3 dir, ForceMode mode)
        {
            dir.Normalize();

            float angle = Mathf.Acos(Vector3.Dot(dir, new Vector3(dir.x, 0f, dir.z).normalized)) * Mathf.Rad2Deg;
            if(angle < 30f && angle > 0f)
            {
                dir = Quaternion.Euler(0f, 0f, 30f - angle) * dir;
            }

            hipRb.AddForce(power * dir, mode);
        }
    }
}