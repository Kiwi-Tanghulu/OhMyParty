using UnityEngine;

namespace OMG.Minigames.CookingClass
{
    public class DispenserMovement : CharacterComponent
    {
        [SerializeField] float moveSpeed = 5f;

        [Space(15f)]
        [SerializeField] float idleTime = 2f;
        [SerializeField] float idleTiemRandomness = 1f;

        [Space(15f)]
        [SerializeField] Vector2 minPos = Vector2.zero;
        [SerializeField] Vector2 maxPos = Vector2.zero;

        private float idleTimer = 0f;
        private Vector3 targetPosition = Vector3.zero;

        public override void Init(CharacterController controller)
        {
            base.Init(controller);
            targetPosition = controller.transform.position;
        }

        public override void UpdateCompo()
        {
            base.UpdateCompo();

            if(Controller.IsOwner == false)
                return;

            if(idleTimer > 0f)
            {
                idleTimer -= Time.deltaTime;
                if(idleTimer > 0f)
                    return;
            }

            Vector3 direction = targetPosition - transform.position;
            float srqDistance = direction.sqrMagnitude;
            if(srqDistance < 0.1f) 
            {
                float x = Random.Range(minPos.x, maxPos.x);
                float z = Random.Range(minPos.y, maxPos.y);
                targetPosition = new Vector3(x, transform.position.y, z);

                idleTimer = idleTime + Random.Range(-idleTiemRandomness, idleTiemRandomness);
            }
            else
            {
                transform.position += direction.normalized * Time.deltaTime * moveSpeed;
            }
        }
    }
}
