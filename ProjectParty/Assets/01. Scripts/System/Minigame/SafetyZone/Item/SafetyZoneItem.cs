using OMG.Items;
using UnityEngine;

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
            collision.OnCollisionPlayerEvent.AddListener(OnCollisionPlayer);
        }
        
        public override void Init()
        {
            base.Init();
            collision.Init();
        }

        public override void OnActive()
        {
            if(IsHost == false)
                return;

            currentHolder.Release();

            collision.SetActiveCollisionOther(true);
            collision.SetActiveRigidbody(true);

            collision.AddForce(transform.forward, 15f);
        }

        public override void OnHold() { }

        public override void OnRelease() { }

        public abstract void OnCollisionPlayer(SafetyZonePlayerController player, Collision other);
        public virtual void OnCollision(Collision other) 
        {
            Destroy(gameObject);
        }
    }
}
