using Cinemachine;
using OMG.Inputs;
using UnityEngine;

namespace OMG.Player
{
    public class TPSPlayerCamera : PlayerCamera
    {
        [SerializeField] private PlayInputSO input;

        [Space]
        [SerializeField] private float turnSpeed;

        [Space]
        [SerializeField] private Vector2 xLimitAngle;

        [Space]
        [SerializeField] private bool invertX;
        [SerializeField] private bool invertY;

        private Vector2 currentRotation;

        private Quaternion forward;
        public Quaternion Forward => forward;

        public override void Init(OMG.CharacterController controller)
        {
            base.Init(controller);

            forward = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
            currentRotation = Vector2.zero;

            input.OnMouseDeltaEvent += Turn;
        }

        private void Update()
        {
            transform.eulerAngles = currentRotation;
        }

        private void OnDestroy()
        {
            input.OnMouseDeltaEvent -= Turn;
        }

        private void Turn(Vector2 mouseDelta)
        {
            Vector2 rotateAngle = new Vector2(mouseDelta.y, mouseDelta.x) * turnSpeed;
            if (invertX) rotateAngle.y *= -1;
            if (invertY) rotateAngle.x *= -1;
            
            currentRotation += rotateAngle;
            currentRotation.x = Mathf.Clamp(currentRotation.x, xLimitAngle.x, xLimitAngle.y);

            forward = Quaternion.Euler(0f, currentRotation.y, 0f);
        }
    }
}
