using Steamworks;
using UnityEngine;
using OMG.Network;

namespace OMG.Test
{
    public class TNetwork : MonoBehaviour
    {
        public void StartClient()
        {
            Debug.Log(SteamClient.SteamId.Value);
            ClientManager.Instance.StartClient(SteamClient.SteamId);
        }

        public void StartHost(int maxMember)
        {
            HostManager.Instance.StartHost(maxMember);
        }
    }
}
