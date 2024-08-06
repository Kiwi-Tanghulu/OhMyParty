namespace OMG.Minigames.PunchClub
{
    public class BoxingGun : CharacterComponent
    {
        private DamageCaster damageCaster = null;
        private BoxingGunAnimator animator = null;

        public override void Init(CharacterController controller)
        {
            base.Init(controller);
            animator = transform.Find("Visual").GetComponent<BoxingGunAnimator>();
            animator.OnAnimationTriggerEvent += HandleAnimationTrigger;
        }

        public void Fire()
        {
            animator.SetFire();
        }

        private void HandleAnimationTrigger()
        {
            if(Controller.IsOwner == false)
                return;

            damageCaster.DamageCast(Controller.transform, 100f);
        }
    }
}