using OMG.FSM;
using OMG.Player.FSM;
using UnityEngine;
using UnityEngine.Events;
using OMG.NetworkEvents;
using Steamworks;
using UnityEngine.InputSystem.XR;
using Unity.Netcode;
using System.Runtime.CompilerServices;

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

        private NetworkEvent<AttackParams> onDamaged = new NetworkEvent<AttackParams>("onDamaged");
        private NetworkEvent<AttackParams> onDamagedNetworkEvent = new NetworkEvent<AttackParams>("onDamagedNetworkEvent");

        public override void Init(CharacterController controller)
        {
            base.Init(controller);

            Hitable = true;
            PlayerHitable = true;

            onDamaged.AddListener(BroadcastOnDamaged);
            onDamaged.Register(controller.NetworkObject);

            onDamagedNetworkEvent.AddListener(BroadcastOnDamagedNetworkEvent);
            onDamagedNetworkEvent.Register(controller.NetworkObject);
        }

        public void OnDamaged(float damage, Transform attacker, Vector3 point,
            HitEffectType effectType, Vector3 normal = default)
        {
            ulong attackerID = 0;
            if (transform.TryGetComponent<NetworkObject>(out NetworkObject networkObject))
                attackerID = networkObject.NetworkObjectId;
            int hitEffectType = (int)effectType;
            this.attacker = attacker;
            this.damage = damage;
            hitPoint = point;
            hitDir = (transform.position - attacker.position).normalized;

            #region !use in network
#if UNITY_EDITOR
            if (!Controller.UseInNetwork)
            {
                if (effectType == HitEffectType.Die)
                {
                    Controller.GetCharacterComponent<CharacterFSM>().ChangeState(typeof(DieState));

                    OnDamagedEvent?.Invoke(HitPoint);
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

                    OnDamagedEvent?.Invoke(HitPoint);
                }
                return;
            }
#endif
            #endregion
            
            onDamaged?.Broadcast(new AttackParams(attackerID, hitEffectType, damage, point, hitDir, normal), false);
        }

        public void BroadcastOnDamaged(AttackParams param)
        {
            if (!Controller.IsOwner)
                return;
            
            attacker = NetworkManager.Singleton.SpawnManager.SpawnedObjects[param.AttackerID].transform;
            damage = param.Damage;
            hitDir = param.Dir;
            hitPoint = param.Point;

            HitEffectType effectType = (HitEffectType)param.EffectType;

            if (effectType == HitEffectType.Die)
            {
                Controller.GetCharacterComponent<CharacterFSM>().ChangeState(typeof(DieState));

                onDamagedNetworkEvent?.Broadcast(param);
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
                
                onDamagedNetworkEvent?.Broadcast(param);
            }
        }
        public void BroadcastOnDamagedNetworkEvent(AttackParams param)
        {
            OnDamagedEvent?.Invoke(param.Point);
        }
    }
}