using UnityEngine;

namespace OMG.Interacting
{
    public interface IInteractable
    {
        public bool Interact(Component performer, bool actived, Vector3 point = default);
    }
}
