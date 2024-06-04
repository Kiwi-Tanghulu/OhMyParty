using OMG.Items;

namespace OMG.Minigames.SafetyZone
{
    public abstract class SafetyZoneItem : HoldableItem
    {
        protected ItemCollision collision = null;

        protected override void Awake()
        {
            base.Awake();
            collision = GetComponent<ItemCollision>();
            collision.OnCollisionEvent.AddListener(OnCollision);
        }

        public override void OnActive()
        {
            collision.SetActiveCollisionOther(true);
            collision.SetActiveRigidbody(true);

            collision.AddForce(transform.forward, 10f);
        }

        public override void OnHold() { }

        public override void OnRelease() { }

        public abstract void OnCollision();
    }
}
