using UnityEngine;

namespace OMG
{
    public class CharacterComponent : MonoBehaviour
    {
        private OMG.CharacterController controller;
        public OMG.CharacterController Controller => controller;

        public virtual void Init(OMG.CharacterController controller)
        {
            this.controller = controller;
        }

        public virtual void UpdateCompo() { }
    }
}