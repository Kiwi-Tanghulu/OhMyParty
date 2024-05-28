using UnityEngine;
using UnityEngine.Events;

namespace OMG.Items
{
    public abstract class Item : MonoBehaviour
    {
        public UnityEvent OnActiveEvent = null;

        public virtual void Active()
        {
            OnActive();
            OnActiveEvent?.Invoke();
        }

        public abstract void OnActive();
    }
}
