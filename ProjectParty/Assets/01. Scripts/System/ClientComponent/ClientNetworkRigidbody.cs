using System.Collections;
using System.Collections.Generic;
using Unity.Netcode.Components;
using UnityEngine;

namespace OMG.Client
{
    public class ClientNetworkRigidbody : NetworkRigidbody
    {
        [SerializeField] private bool isKineticOnSpawned;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            GetComponent<Rigidbody>().isKinematic = isKineticOnSpawned;
        }
    }
}
