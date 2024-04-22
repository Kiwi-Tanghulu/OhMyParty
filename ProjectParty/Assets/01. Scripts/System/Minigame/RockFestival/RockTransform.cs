using OMG.Utility.Transforms;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace OMG.Minigames.RockFestival
{
    public class RockTransform : NetworkBehaviour
    {
        [SerializeField] LayerMask groundLayer = 0;

        private NetworkTransform networkTransform = null;
        public NetworkTransform NetworkTransform => networkTransform;

        private SubstituteParent parent = null;

        private void Awake()
        {
            parent = GetComponent<SubstituteParent>();
            networkTransform = GetComponent<NetworkTransform>();
        }

        public void FitToGround()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, float.MaxValue, groundLayer))
                SetPositionImmediately(hit.point + (transform.forward * 0.5f) + (Vector3.up * 0.5f));
        }

        public void SetPositionImmediately(Vector3 position)
        {
            networkTransform.Teleport(position, transform.rotation, transform.lossyScale);
        }

        public void SetParent(Transform newParent)
        {
            parent.SetParent(newParent);
        }
    }
}
