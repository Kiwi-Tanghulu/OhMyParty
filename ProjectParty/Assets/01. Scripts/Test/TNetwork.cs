using UnityEngine;
using OMG.Network;
using Unity.Netcode;
using System;
using OMG.Networks;

namespace OMG.Test
{
    public class TNetwork : MonoBehaviour
    {
        public void StartHost(int maxMember)
        {
            HostManager.Instance.StartHost(maxMember);
        }
    }
}
