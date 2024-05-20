using UnityEngine;

namespace OMG.UI.Solid
{
    public abstract class SolidUI : MonoBehaviour
    {
        public virtual bool Active { get => active; set => active = value; }
        [SerializeField] protected bool active = true;
    }
}
