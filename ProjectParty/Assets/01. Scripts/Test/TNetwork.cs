using UnityEngine;
using OMG.Network;

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
