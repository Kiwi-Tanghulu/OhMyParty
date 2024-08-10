using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Netcode;
using Unity.Netcode.Components;
using Unity.Netcode;

namespace OMG
{
    public class OwnerNetworkAnimator : NetworkAnimator
    {
        [SerializeField] private NetworkObject networkObject;

        protected override bool OnIsServerAuthoritative()
        {
            return networkObject.IsOwner;
        }
    }
}
