using OMG.Extensions;
using OMG.NetworkEvents;
using System;
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

        //[Space]
        //[SerializeField] protected bool onInitActive;
        protected bool active;

        [Space]
        public UnityEvent OnActiveEvent;
        public UnityEvent OnDeactiveEvent;

        private NetworkEvent<BoolParams> setActiveRagdollRpc = new NetworkEvent<BoolParams>("SetActiveRagdollRpc");
        private NetworkEvent<AttackParams> addForceRpc = new NetworkEvent<AttackParams>("addForceRpc");

        private Action addforceStorage;

        protected RagdollPart[] parts;

        public override void Init(CharacterController controller)
        {
            base.Init(controller);

            setActiveRagdollRpc.Register(controller.NetworkObject);
            setActiveRagdollRpc.AddListener(SetActive);
            addForceRpc.Register(controller.NetworkObject);
            addForceRpc.AddListener(AddForce);

            parts = GetComponentsInChildren<RagdollPart>();
            for (int i = 0; i < parts.Length; i++)
                parts[i].Init(copyTargetRoot.FindFromAll(parts[i].gameObject.name));

            OnActiveEvent.AddListener(() =>
            {
                addforceStorage?.Invoke();
                addforceStorage = null;
            });

            SetActive(false);
            //gameObject.SetActive(onInitActive);
        }

        public void SetActive(bool value)
        {
            if (!Controller.IsOwner)
            {
                Debug.LogError("Only Can Call Owenr");
                return;
            }

#if UNITY_EDITOR
            if (Controller.UseInNetwork)
                setActiveRagdollRpc.Broadcast(value);
            else
                setActiveRagdollRpc.Invoke(value);
#else
            setActiveRagdollRpc.Broadcast(value);
#endif
        }

        protected virtual void SetActive(BoolParams value)
        {
            //gameObject.SetActive(value);

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].Col.enabled = value;
                parts[i].Rb.isKinematic = !value;
            }

            if (value)
            {
                for (int i = 0; i < parts.Length; i++)
                {
                    parts[i].Rb.velocity = Vector3.zero;
                    parts[i].Rb.angularVelocity = Vector3.zero;
                    parts[i].Copy();
                }

                OnActiveEvent?.Invoke();
            }
            else
            {
                OnDeactiveEvent?.Invoke();
            }

            active = value;
        }

        public void AddForce(float power, Vector3 dir)
        {
            if (!Controller.IsOwner)
            {
                Debug.LogError("Only Can Call Owenr");
                return;
            }

            AttackParams param = new AttackParams();
            param.Damage = power;
            param.Dir = dir;

#if UNITY_EDITOR
            if (Controller.UseInNetwork)
                addForceRpc.Broadcast(param);
            else
                addForceRpc.Invoke(param);
#else
            addForceRpc.Broadcast(param);
#endif
        }

        private void AddForce(AttackParams param)
        {
            Vector3 dir = param.Dir;
            float power = param.Damage;
            dir.Normalize();

            float angle = Mathf.Acos(Vector3.Dot(dir, new Vector3(dir.x, 0f, dir.z).normalized)) * Mathf.Rad2Deg;
            if(angle < 30f && angle > 0f)
            {
                dir = Quaternion.Euler(0f, 0f, 30f - angle) * dir;
            }

            if (active)
            {
                hipRb.AddForce(power * dir, ForceMode.Impulse);
                addforceStorage = null;
            }
            else
            {
                addforceStorage = () => hipRb.AddForce(power * dir);
            }
        }
    }
}