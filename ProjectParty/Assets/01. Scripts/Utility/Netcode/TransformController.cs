using OMG.Utility.Transforms;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

namespace OMG.Utility.Netcodes
{
    public class TransformController : NetworkBehaviour
    {
        protected NetworkTransform networkTransform = null;
        public NetworkTransform NetworkTransform => networkTransform;

        private SubstituteParent parent = null;

        private void Awake()
        {
            parent = GetComponent<SubstituteParent>();
            networkTransform = GetComponent<NetworkTransform>();
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
