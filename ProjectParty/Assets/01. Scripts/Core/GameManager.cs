using Netcode.Transports.Facepunch;
using UnityEngine;
using OMG.Network;
using Unity.Netcode;

namespace OMG
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; } = null;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            InitNetwork();
        }

        private void OnApplicationQuit()
        {
            GuestManager.Instance?.Disconnect();
            HostManager.Instance?.Disconnect();    
        }

        private void InitNetwork()
        {
            ClientManager.Instance = new ClientManager();
            HostManager.Instance = new HostManager();
            GuestManager.Instance = new GuestManager(NetworkManager.Singleton.GetComponent<FacepunchTransport>());
        }
    }
}
