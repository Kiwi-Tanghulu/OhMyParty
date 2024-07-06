using OMG.FSM;
using OMG.Player.FSM;
using UnityEngine;
using UnityEngine.Events;
using OMG.NetworkEvents;
using Steamworks;
using UnityEngine.InputSystem.XR;

namespace OMG.Player
{
    public class PlayerHealth : CharacterComponent, IDamageable
    {
        private Transform attacker;
        private float damage;
        private Vector3 hitDir;
        private Vector3 hitPoint;

        public UnityEvent<Vector3 /*hit point*/> OnDamagedEvent;

        public Transform Attacker => attacker;
        public float Damage => damage;
        public Vector3 HitDir => hitDir;
        public Vector3 HitPoint => hitPoint;

        public bool Hitable;
        public bool PlayerHitable;

        private NetworkEvent<Vector3Params> hitEvent = new NetworkEvent<Vector3Params>("hitEvent");

        public override void Init(CharacterController controller)
        {
            base.Init(controller);

            Hitable = true;
            PlayerHitable = true;

            hitEvent.AddListener(BroadcastOnDamagedEvent);
            hitEvent.Register(controller.NetworkObject);
        }

        public void OnDamaged(float damage, Transform attacker, Vector3 point,
            HitEffectType effectType, Vector3 normal = default)
        {
            this.attacker = attacker;
            this.damage = damage;
            hitDir = (transform.position - attacker.position).normalized;
            hitPoint = point;

            hitDir = -transform.forward; //test

            #region !use in network
#if UNITY_EDITOR
            if(!Controller.UseInNetwork)
            {
                if (effectType == HitEffectType.Die)
                {
                    Controller.GetCharacterComponent<CharacterFSM>().ChangeState(typeof(DieState));

                    OnDamagedEvent?.Invoke(new Vector3Params(HitPoint));
                }
                else
                {
                    if (!Hitable)
                        return;

                    if (!PlayerHitable)
                    {
                        if (attacker.TryGetComponent<PlayerController>(out PlayerController player))
                        {
                            return;
                        }
                    }

                    switch (effectType)
                    {
                        case HitEffectType.None:
                            break;
                        case HitEffectType.Stun:
                            Controller.GetCharacterComponent<CharacterFSM>().ChangeState(typeof(StunState));
                            break;
                        case HitEffectType.Knockback:
                            Controller.GetCharacterComponent<CharacterFSM>().ChangeState(typeof(KnockbackState));
                            break;
                    }

                    OnDamagedEvent?.Invoke(new Vector3Params(HitPoint));
                }
                return;
            }
#endif
            #endregion

            if (effectType == HitEffectType.Die)
            {
                Controller.GetCharacterComponent<CharacterFSM>().ChangeState(typeof(DieState));

                hitEvent?.Broadcast(new Vector3Params(HitPoint));
            }
            else
            {
                if (!Hitable)
                    return;

                if (!PlayerHitable)
                {
                    if (attacker.TryGetComponent<PlayerController>(out PlayerController player))
                    {
                        return;
                    }
                }

                switch (effectType)
                {
                    case HitEffectType.None:
                        break;
                    case HitEffectType.Stun:
                        Controller.GetCharacterComponent<CharacterFSM>().ChangeState(typeof(StunState));
                        break;
                    case HitEffectType.Knockback:
                        Controller.GetCharacterComponent<CharacterFSM>().ChangeState(typeof(KnockbackState));
                        break;
                }

                hitEvent?.Broadcast(new Vector3Params(HitPoint));
            }
        }

        public void BroadcastOnDamagedEvent(Vector3Params param)
        {
            OnDamagedEvent?.Invoke(param.Value);
        }
    }
}