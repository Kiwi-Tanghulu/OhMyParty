using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Items
{
    public class NetworkItemBehaviour : NetworkBehaviour
    {
        [SerializeField] List<NetworkItemEvent> itemEvents;
        private NetworkItem item = null;

        private void Awake()
        {
            item = GetComponent<NetworkItem>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            item.OnSpawnedEvent?.Invoke();
        }

        public void BroadcastEvent(string eventName)
        {
            int index = itemEvents.FindIndex(i => i.EventName == eventName);
            if(index == -1)
                return;

            BroadcastEventServerRpc(index);
        }

        [ServerRpc(RequireOwnership = false)]
        private void BroadcastEventServerRpc(int index) => BroadcastEventClientRpc(index);
        [ClientRpc]
        private void BroadcastEventClientRpc(int index) => itemEvents[index].Event?.Invoke();

        public void ActiveItem()
        {
            ActiveServerRpc();
        }

        [ServerRpc]
        private void ActiveServerRpc()
        {
            ActiveClientRpc();
        }

        [ClientRpc]
        private void ActiveClientRpc()
        {
            item.OnActive();
            item.OnActiveEvent?.Invoke();
        }
    }
}
