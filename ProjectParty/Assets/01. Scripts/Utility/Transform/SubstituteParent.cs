using UnityEngine;

namespace OMG.Utility.Transforms
{
    public class SubstituteParent : MonoBehaviour
    {
        private Transform parent = null;

        private Vector3 prevPosition;
        private Quaternion prevRotation;

        private bool activeRotate = true;
        private bool activeTranslate = true;

        private void FixedUpdate()
        {
            if (parent == null)
                return;

            if (activeTranslate)
                UpdatePosition();
            if(activeRotate)
                UpdateRotation();
        }

        private void UpdatePosition()
        {           
            if (prevPosition != parent.position)
            {
                prevPosition = parent.position;
                transform.position = prevPosition;
            }
        }

        private void UpdateRotation()
        {
            if (prevRotation != parent.rotation)
            {
                prevRotation = parent.rotation;
                transform.rotation = prevRotation;
            }
        }

        public void SetParent(Transform parent)
        {
            this.parent = parent;
        }

        public void SetActiveRotate(bool active)
        {
            activeRotate = active;
        }

        public void SetActiveTranslate(bool active)
        {
            activeTranslate = active;
        }
    }
}
