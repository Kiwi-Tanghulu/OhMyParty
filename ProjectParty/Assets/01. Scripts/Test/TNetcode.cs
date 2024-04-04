using Unity.Netcode;
using UnityEngine;

namespace OMG.Test
{
    public class TNetcode : MonoBehaviour
    {
        public void StartClient()
        {
            NetworkManager.Singleton.StartClient();
        }

        public void StartHost()
        {
            NetworkManager.Singleton.StartHost();
        }        
    }
}
