using Netcode.Transports.Facepunch;
using OMG.Datas;
using OMG.Networks;
using OMG.Networks.Steam;
using OMG.Networks.UGS;
using Steamworks;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] DataLoader dataLoader = null;

        [Space(15f)]
        [SerializeField] NetworkManager networkManager = null;
        [SerializeField] NetworkServiceType networkServiceType = NetworkServiceType.None;

        [Space(15f)]
        [SerializeField] GameObject LoginErrorUI;

        private async void Start()
        {
            dataLoader.LoadData();

            HostManager hostManager = null;
            GuestManager guestManager = null;
            bool response = false;
            string nickname = DataManager.UserData.SettingData.Nickname;

            switch (networkServiceType)
            {
                case NetworkServiceType.Steam:
                {
                    response = SetNetworkTransport<FacepunchTransport>().Init();
                    if(response == false)
                        break;

                    if(string.IsNullOrEmpty(nickname))
                        nickname = SteamClient.Name;

                    hostManager = new SteamHostManager();
                    guestManager = new SteamGuestManager();
                }
                    break;
                case NetworkServiceType.UGS:
                {
                    await UnityServices.InitializeAsync();
                    await AuthenticationService.Instance.SignInAnonymouslyAsync();
                    response = AuthenticationService.Instance.IsSignedIn && AuthenticationService.Instance.IsAuthorized;
                    if (response == false)
                        break;

                    SetNetworkTransport<UnityTransport>();

                    if(string.IsNullOrEmpty(nickname))
                        nickname = "unknown";

                    hostManager = new UGSHostManager();
                    guestManager = new UGSGuestManager();
                    response = true;
                }
                    break;
            }

            if(response)
            {
                DataManager.UserData.SettingData.Nickname = nickname;
                ClientManager.Instance.SetNickname(nickname);
                
                GameManager.Instance.InitNetwork(hostManager, guestManager);
                SceneManager.Instance.LoadScene(SceneType.IntroScene);
            }
            else
            {
                LoginErrorUI.SetActive(true);
            }
        }

        private T SetNetworkTransport<T>() where T : NetworkTransport
        {
            T transport = networkManager.AddComponent<T>();
            networkManager.NetworkConfig.NetworkTransport = transport;
            ClientManager.Instance.NetworkTransport = transport;

            return transport;
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
