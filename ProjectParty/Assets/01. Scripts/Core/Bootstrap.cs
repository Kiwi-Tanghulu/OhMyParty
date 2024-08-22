using Netcode.Transports.Facepunch;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] bool localHost = false;

        [SerializeField] GameObject LoginErrorUI;

        private void Start()
        {
            if(localHost)
            {
                UnityTransport transport = NetworkManager.Singleton.AddComponent<UnityTransport>();
                NetworkManager.Singleton.NetworkConfig.NetworkTransport = transport;

                GameManager.Instance.InitNetwork();
                SceneManager.Instance.LoadScene(SceneType.IntroScene);
            }
            else
            {
                FacepunchTransport transport = NetworkManager.Singleton.AddComponent<FacepunchTransport>();
                NetworkManager.Singleton.NetworkConfig.NetworkTransport = transport;

                if (transport.Init())
                {
                    GameManager.Instance.InitNetwork();
                    SceneManager.Instance.LoadScene(SceneType.IntroScene_Steam);
                }
                else
                {
                    Debug.Log(2);
                    // ³¡³»±â
                    LoginErrorUI.SetActive(true);
                }
            }
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
