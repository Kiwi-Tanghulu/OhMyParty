using System.Collections.Generic;
using UnityEngine;

namespace OMG.Utility.OneWayPlatform
{
    [RequireComponent(typeof(BoxCollider))]
    public partial class OneWayPlatform : MonoBehaviour
    {
        [SerializeField] Direction directionType = Direction.Forward;
        [SerializeField] LayerMask castingLayer = 0;
        [SerializeField] float castColliderScaleFactor = 1.3f;

        private new BoxCollider collider = null;
        private BoxCollider collisionTriggerCollider = null;

        private void Awake()
        {
            collider = GetComponent<BoxCollider>();
            collider.isTrigger  = false;
        
            collisionTriggerCollider = gameObject.AddComponent<BoxCollider>();
            collisionTriggerCollider.size = collider.size * castColliderScaleFactor;
            collisionTriggerCollider.center = collider.center;
            collisionTriggerCollider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            LayerMask otherLayer = 1 << other.gameObject.layer;
            if((otherLayer & castingLayer) == 0)
                return;
            
            bool isBlocked = IsBlocked(other.transform);
            Debug.Log(isBlocked);
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

        private bool IsBlocked(Transform other)
        {
            Vector3 wayDirection = GetDirection(directionType);
            Vector3 otherDirection = other.position - transform.position;

            float dot = Vector3.Dot(wayDirection.normalized, otherDirection.normalized);
            float angle = Mathf.Abs(Mathf.Acos(dot) * Mathf.Rad2Deg);
            Debug.Log(angle);

            bool isBlocked = angle >= 90f;
            return isBlocked;
        }

        private Vector3 GetDirection(Direction directionType)
        {
            Vector3 direction = Vector3.zero;
            switch(directionType)
            {
                case Direction.Forward:
                case Direction.Backward:
                    direction = transform.forward;
                    break;
                case Direction.Up:
                case Direction.Down:
                    direction = transform.up;
                    break;
                case Direction.Left:
                case Direction.Right:
                    direction = transform.right;
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
            Gizmos.DrawLine(transform.position, transform.position + GetDirection(directionType));
        }

        #endif
    }
}
