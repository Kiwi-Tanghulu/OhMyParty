using OMG.Interacting;
using OMG.Items;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.RockFestival
{
    public class Rock : HoldableItem, IFocusable
    {
        [SerializeField] int point = 1;
        public int Point => point;

        [SerializeField] UnityEvent onThrowEvent = null;

        public GameObject CurrentObject => gameObject;

        private RockTransform rockTransform = null;
        private RockCollision rockCollision = null;

        protected override void Awake()
        {
            base.Awake();

            rockCollision = GetComponent<RockCollision>();
            rockTransform = transformController as RockTransform;
        }

        public override void Init()
        {
            base.Init();
            rockCollision.Init();
        }

        public override void OnActive()
        {
            Vector3 direction = currentHolder.HoldingParent.forward + Vector3.up * 0.5f;
            rockCollision.SetActiveCollisionOther(true);
            rockCollision.AddForce(direction, 12.5f);

            currentHolder.Release();
            onThrowEvent?.Invoke();
        }

        public override void OnHold()
        {
            rockCollision.SetActiveRigidbody(false);
        }

        public override void OnRelease()
        {
            if(rockCollision.ActiveCollisionOther == false)
                rockTransform.FitToGround();
            rockCollision.SetActiveRigidbody(true);
        }

        public void OnFocusBegin(Vector3 point)
        {
            
        }

        public void OnFocusEnd()
        {
            
        }
    }
}
