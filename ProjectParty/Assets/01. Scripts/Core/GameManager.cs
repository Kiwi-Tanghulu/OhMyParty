using Netcode.Transports.Facepunch;
using Unity.Netcode;
using UnityEngine;

namespace OMG
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; } = null;

        private void Awake()
        {
            bool init = CreateSingleton();
            if(init == false)
                return;
        }

        private void OnApplicationQuit()
        {
            ClientManager.Instance?.Disconnect();
            HostManager.Instance?.Disconnect();    
        }

        private bool CreateSingleton()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return false;
            }

            Instance = this;

            ClientManager.Instance = new ClientManager(GetComponent<FacepunchTransport>());
            HostManager.Instance = new HostManager();

            return true;
        }
    }
}
