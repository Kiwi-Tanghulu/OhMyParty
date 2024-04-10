using UnityEngine;

namespace OMG.Interacting
{
    public interface IHoldable
    {
        public IHolder CurrentHolder { get; }

        public bool Hold(IHolder holder, Vector3 point = default);
        public IHolder Release();
    }
}
