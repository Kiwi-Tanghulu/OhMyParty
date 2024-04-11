using UnityEngine;

namespace OMG.Interacting
{
    public interface IHolder
    {
        public Transform HoldingParent { get; }
        public IHoldable HoldingObject { get; }

        public bool IsEmpty { get; }

        public bool Hold(IHoldable target, Vector3 point = default);
        public void Active();
        public IHoldable Release();
    }
}
