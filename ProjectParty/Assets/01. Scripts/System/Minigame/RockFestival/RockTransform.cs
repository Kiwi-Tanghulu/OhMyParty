using OMG.Interacting;
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
        private IHolder holderCache = null;
        private ulong clientIDCache = 0;

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

        public void SetParent(ulong clientID, IHolder holder)
        {
            holderCache = holder;
            clientIDCache = clientID;
            SetParentServerRpc(clientID);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetParentServerRpc(ulong clientID)
        {
            NetworkObject.ChangeOwnership(clientID);
            SetParentClientRpc(clientID);
        }

        [ClientRpc]
        private void SetParentClientRpc(ulong clientID)
        {
            if(NetworkManager.Singleton.LocalClientId != clientID)
                return;

            SetPositionImmediately(holderCache.HoldingParent.position);
            parent.SetParent(holderCache.HoldingParent);
        }

        public void ReleaseParent()
        {
            ReleaseParentServerRpc();
        }
        
        [ServerRpc(RequireOwnership = false)]
        private void ReleaseParentServerRpc()
        {
            NetworkObject.ChangeOwnership(NetworkManager.ServerClientId);
            ReleaseParentClientRpc();
        }

        [ClientRpc]
        private void ReleaseParentClientRpc()
        {
            if(clientIDCache != NetworkManager.Singleton.LocalClientId)
                return;

            parent.SetParent(null);
        }
    }
}
