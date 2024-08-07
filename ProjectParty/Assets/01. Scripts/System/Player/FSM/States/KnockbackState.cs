using OMG.FSM;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class KnockbackState : FSMState
    {
        [SerializeField] private float knockbackTime;
        [SerializeField] private float powerMultiplier;

        private PlayerMovement movement;
        private PlayerHealth health;

        public override void InitState(CharacterFSM brain)
        {
            base.InitState(brain);

            movement = brain.Controller.GetCharacterComponent<PlayerMovement>();
            health = brain.Controller.GetCharacterComponent<PlayerHealth>();
        }

        public override void EnterState()
        {
            base.EnterState();

            PlayerMoveType moveType = movement.MoveType;
            movement.MoveType = PlayerMoveType.TopDown;
            movement.Knockback(health.HitDir, health.Damage, knockbackTime, () =>
            {
                brain.ChangeState(brain.DefaultState);
            });
            movement.MoveType = moveType;
        }
    }
}