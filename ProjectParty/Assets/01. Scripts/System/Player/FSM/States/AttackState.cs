using UnityEngine;

namespace OMG.Player.FSM
{
    public class AttackState : ActionState
    {
        [Space]
        [SerializeField] private DamageCaster damageCaster;

        public override void DoAction()
        {
            base.DoAction();
            
            if(damageCaster != null)
            {
                damageCaster.DamageCast(player.transform, 200f);
            }
        }
    }
}