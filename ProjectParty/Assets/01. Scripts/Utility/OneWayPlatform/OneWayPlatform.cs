using OMG.Extensions;
using UnityEngine;

namespace OMG.Utility.OneWayPlatform
{
    [RequireComponent(typeof(BoxCollider))]
    public class OneWayPlatform : MonoBehaviour
    {
        [SerializeField] Direction directionType = Direction.Forward;
        [SerializeField] LayerMask castingLayer = 0;
        [SerializeField] float castColliderPaddingFactor = 0.9f;
        [SerializeField] float castColliderScale = 0.25f;

        public Direction DirectionType {
            get => directionType;
            set {
                directionType = value;
                SettingTriggerCollider(directionType);
            }
        }

        private new BoxCollider collider = null;
        private BoxCollider collisionTriggerCollider = null;

        private void Awake()
        {
            collider = GetComponent<BoxCollider>();
            collider.isTrigger  = false;
        
            collisionTriggerCollider = gameObject.AddComponent<BoxCollider>();
            SettingTriggerCollider(DirectionType);
        }

        private void OnTriggerEnter(Collider other)
        {
            LayerMask otherLayer = 1 << other.gameObject.layer;
            if((otherLayer & castingLayer) == 0)
                return;
            
            bool isBlocked = IsBlocked(other.transform);
            Physics.IgnoreCollision(collider, other, !isBlocked);
        }

        private void OnTriggerExit(Collider other)
        {
            LayerMask otherLayer = 1 << other.gameObject.layer;
            if((otherLayer & castingLayer) == 0)
                return;

            bool isBlocked = IsBlocked(other.transform);
            Debug.Log(isBlocked);
            Physics.IgnoreCollision(collider, other, !isBlocked);
        }

        private void SettingTriggerCollider(Direction directionType)
        {
            Vector3 direction = GetDirection(directionType, Space.World);

            Vector3 originSize = collider.size * castColliderPaddingFactor;
            Vector3 additionalSize = direction.GetAbs() * castColliderScale;
            collisionTriggerCollider.size = originSize + additionalSize;

            Vector3 originCenter = collider.center;
            Vector3 additionalCenter = (collisionTriggerCollider.size - collider.size).GetMultipleEach(direction) * 0.5f;
            collisionTriggerCollider.center = originCenter + additionalCenter;

            collisionTriggerCollider.isTrigger = true;
        }

        private bool IsBlocked(Transform other)
        {
            Vector3 wayDirection = GetDirection(DirectionType, Space.Self);
            Vector3 otherDirection = other.position - transform.position;

            float dot = Vector3.Dot(wayDirection.normalized, otherDirection.normalized);
            float angle = Mathf.Abs(Mathf.Acos(dot) * Mathf.Rad2Deg);

            bool isBlocked = angle >= 90f;
            return isBlocked;
        }

        private Vector3 GetDirection(Direction directionType, Space space)
        {
            Vector3 direction = Vector3.zero;
            switch(directionType)
            {
                case Direction.Forward:
                case Direction.Backward:
                    direction = space == Space.Self ? transform.forward : Vector3.forward;
                    break;
                case Direction.Up:
                case Direction.Down:
                    direction = space == Space.Self ? transform.up : Vector3.up;
                    break;
                case Direction.Left:
                case Direction.Right:
                    direction = space == Space.Self ? transform.right : Vector3.right;
                    break;
            }

            if((int)directionType % 2 == 1)
                direction = -direction;

            return direction;
        }

        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + GetDirection(directionType, Space.Self));
        }

        #endif
    }
}
