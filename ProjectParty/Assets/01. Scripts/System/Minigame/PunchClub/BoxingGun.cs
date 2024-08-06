using UnityEngine;

namespace OMG.Minigames.PunchClub
{
    public class BoxingGun : CharacterComponent
    {
        private DamageCaster damageCaster = null;
        private Animator animator = null;

        public override void Init(CharacterController controller)
        {
            base.Init(controller);
            animator = GetComponent<Animator>();
        }

        public void Fire()
        {
            animator.SetTrigger("Fire");
        }

        public void TriggerEvent()
        {
            if(Controller.IsOwner == false)
                return;

            damageCaster.DamageCast(Controller.transform, 100f);
        }
    }
}