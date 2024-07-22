using OMG.NetworkEvents;
using OMG.Player;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class OhMySwordPlayerHealth : PlayerHealth
    {
        [SerializeField] NetworkEvent<Vector3Params, Vector3> onAttackFailedEvent = new NetworkEvent<Vector3Params, Vector3>("attackFail");

        public override void Init(CharacterController controller)
        {
            base.Init(controller);
            onAttackFailedEvent.Register(controller.NetworkObject);
        }

        public override void OnDamaged(float damage, Transform attacker, Vector3 point, HitEffectType effectType, Vector3 normal = default)
        {
            if(attacker.TryGetComponent<CatchTailPlayer>(out CatchTailPlayer otherPlayer) == false)
                return;

            if(otherPlayer.IsCorrectTarget(Controller.NetworkObject.OwnerClientId) == false)
            {
                onAttackFailedEvent.Broadcast(point);
                return;
            }

            base.OnDamaged(damage, attacker, point, effectType, normal);
        }
    }
}
