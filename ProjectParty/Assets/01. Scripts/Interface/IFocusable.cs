using UnityEngine;

namespace OMG.Interacting
{
    public interface IFocusable
    {
        public GameObject CurrentObject { get; }

        public void OnFocusBegin(Vector3 point);
        public void OnFocusEnd();
    }
}
