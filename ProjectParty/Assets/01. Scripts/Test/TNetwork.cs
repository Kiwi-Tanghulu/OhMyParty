using UnityEngine;
using OMG.Network;
using Unity.Netcode;
using System;

namespace OMG.Test
{
    public class TNetwork : MonoBehaviour
    {
        public void StartHost(int maxMember)
        {
            HostManager_.Instance.StartHost(maxMember);
        }
    }
}
