using System;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Utility.Netcodes
{
    public class OwnershipController : NetworkBehaviour
    {
        private ulong clientIDCache = ulong.MaxValue;
        private Action ownerCallbackCache = null;
        private Action broadcastCallbackCache = null;

        public void SetOwner(ulong clientID, Action ownerCallback = null, Action broadcastCallback = null)
        {
            clientIDCache = clientID;
            ownerCallbackCache = ownerCallback;
            broadcastCallbackCache = broadcastCallback;

            SetOwnerServerRpc(clientID);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SetOwnerServerRpc(ulong clientID)
        {
            NetworkObject.ChangeOwnership(clientID);
            SetOwnerClientRpc();
        }

        [ClientRpc]
        private void SetOwnerClientRpc()
        {
            // OnOwnershipChanged 을 사용하지 않는 이유는 메소드의 흐름을 좀 더 직관적으로 확인하기 위해서.
            if(IsChangedOwner())
                ownerCallbackCache?.Invoke();

            broadcastCallbackCache?.Invoke();
            ClearCache();
        }

        private void ClearCache()
        {
            ownerCallbackCache = null;
            clientIDCache = ulong.MaxValue;
        }

        private bool IsChangedOwner()
        {
           return clientIDCache == OwnerClientId; 
        }
    }
}
