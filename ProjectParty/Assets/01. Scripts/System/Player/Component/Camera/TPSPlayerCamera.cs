using Cinemachine;
using OMG.Inputs;
using System.Collections;
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
        [SerializeField] private float dampingWeightMultiplier;
        private float dampingWeight;
        private Coroutine dampingClampCo;
        [SerializeField]private int startClampZeroCount = 0;
        private int zeroCount = 0;

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

            //input.OnMouseDeltaEvent += Turn;
        }

        private void Update()
        {
            Turn();
        }

        private void LateUpdate()
        {
            transform.position = Controller.transform.position;
        }

        private void OnDestroy()
        {
            //input.OnMouseDeltaEvent -= Turn;
        }

        private void Turn()
        {
            Vector2 mouseDelta = input.MouseDelta;

            if (mouseDelta == Vector2.zero)
                zeroCount++;

            Vector2 rotateAngle = new Vector2(mouseDelta.y, mouseDelta.x) * turnSpeed;
            if (invertX) rotateAngle.y *= -1;
            if (invertY) rotateAngle.x *= -1;
            
            currentRotation += rotateAngle;
            
            if(mouseDelta != Vector2.zero && dampingClampCo != null)
            {
                StopCoroutine(dampingClampCo);
                dampingClampCo = null;
            }

            float clampedValue = Mathf.Clamp(currentRotation.x, xLimitAngle.x, xLimitAngle.y);
            if (clampedValue != currentRotation.x)
            {
                dampingWeight = Mathf.Max(1f, 
                    Mathf.Max(clampedValue, currentRotation.x) - Mathf.Min(clampedValue, currentRotation.x));

                currentRotation.x = (currentRotation.x - rotateAngle.x) + (rotateAngle.x * (1f / dampingWeight));

                if (zeroCount > startClampZeroCount && dampingClampCo == null)
                {
                    dampingClampCo = StartCoroutine(DampingClampCo(clampedValue));
                }
            }

            transform.eulerAngles = currentRotation;

            forward = Quaternion.Euler(0f, currentRotation.y, 0f);
        }

        private IEnumerator DampingClampCo(float clampedValue)
        {
            zeroCount = 0;

            while(true)
            {
                dampingWeight = Mathf.Max(clampedValue, currentRotation.x) - Mathf.Min(clampedValue, currentRotation.x);

                currentRotation.x = Mathf.Lerp(currentRotation.x, clampedValue, Time.deltaTime * dampingWeight * dampingWeightMultiplier);

                transform.eulerAngles = currentRotation;

                yield return null;
            }
        }
    }
}
