using OMG.Interacting;
using OMG.Items;
using Unity.Netcode;
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
        private RockBehaviour rockBehaviour = null;

        public ulong HolderID => rockBehaviour.HolderID;

        protected override void Awake()
        {
            base.Awake();

            rockCollision = GetComponent<RockCollision>();
            rockTransform = transformController as RockTransform;
            rockBehaviour = itemBehaviour as RockBehaviour;
        }

        public override void Init()
        {
            base.Init();
            rockCollision.Init();
        }

        public override void OnActive()
        {
            if(IsOwner == false)
                return;

            Vector3 direction = currentHolder.HoldingParent.forward + Vector3.up * 0.5f;
            rockCollision.SetActiveCollisionOther(true);
            rockCollision.AddForce(direction, 12.5f);

            currentHolder.Release();
            onThrowEvent?.Invoke();
        }

        public override bool Hold(IHolder holder, Vector3 point = default)
        {
            if (rockBehaviour.HolderID != ulong.MaxValue)
                return false;

            return base.Hold(holder, point);
        }

        public override void OnHold()
        {
            rockBehaviour.Hold(currentHolder.HolderObject.GetComponent<NetworkObject>().OwnerClientId);
            rockCollision.SetActiveRigidbody(false);
        }

        public override void OnRelease()
        {
            rockBehaviour.Hold(ulong.MaxValue);
            if(rockCollision.ActiveCollisionOther == false)
                rockTransform.FitToGround();
            rockCollision.SetActiveRigidbody(true);
        }

        public void OnFocusBegin(Vector3 point) { }

        public void OnFocusEnd() { }
    }
}
