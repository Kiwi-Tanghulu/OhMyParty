using Netcode.Transports.Facepunch;
using OMG.Networks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] NetworkManager networkManager = null;
        [SerializeField] NetworkServiceType networkServiceType = NetworkServiceType.None;

        [SerializeField] GameObject LoginErrorUI;

        private void Start()
        {
            HostManager hostManager = null;
            GuestManager guestManager = null;
            bool response = false;

            switch(networkServiceType)
            {
                case NetworkServiceType.Steam:
                    FacepunchTransport transport = networkManager.AddComponent<FacepunchTransport>();
                    networkManager.NetworkConfig.NetworkTransport = transport;
                    response = transport.Init();

                    if(response == false)
                        break;

                    hostManager = new SteamHostManager();
                    guestManager = new SteamGuestManager();
                    break;
                case NetworkServiceType.UGS:

                    break;
            }

            if(response)
            {
                GameManager.Instance.InitNetwork(hostManager, guestManager);
                SceneManager.Instance.LoadScene(SceneType.IntroScene);
            }
            else
            {
                LoginErrorUI.SetActive(true);
            }
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
