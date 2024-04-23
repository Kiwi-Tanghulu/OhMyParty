using UnityEngine;

namespace OMG.UI.Solid
{
    public abstract class SolidUI : MonoBehaviour
    {
        [field : SerializeField]
        public bool Active { get; protected set; } = true;
    }
}
