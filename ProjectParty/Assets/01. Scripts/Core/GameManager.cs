using Netcode.Transports.Facepunch;
using UnityEngine;
using OMG.Network;
using Unity.Netcode;
using Steamworks;

namespace OMG
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; } = null;

        public bool CursorActive {
            get => Cursor.visible;
            set {
                Cursor.visible = value;
                Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
            }
        }

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitSingleton();
            DEFINE.GlobalAudioPlayer = GetComponent<AudioSource>();
        }

        private void Start()
        {
            InitNetwork();
        }

        private void OnApplicationQuit()
        {
            ClientManager.Instance?.Disconnect();

            GuestManager.Instance?.Release();
            HostManager.Instance?.Release();
            ClientManager.Instance?.Release();
        }

        private void InitSingleton()
        {
            SceneManager.Instance = gameObject.AddComponent<SceneManager>();
            CameraManager.CreateSingleton(gameObject);
        }

        private void InitNetwork()
        {
            ClientManager.Instance = new ClientManager();
            HostManager.Instance = new HostManager();
            GuestManager.Instance = new GuestManager(NetworkManager.Singleton.GetComponent<FacepunchTransport>());

            ClientManager.Instance.OnDisconnectEvent += HandleDisconnect;
        }

        private void HandleDisconnect()
        {
            SceneType scene = SteamClient.IsValid ? SceneType.IntroScene_Steam : SceneType.IntroScene;
            SceneManager.Instance?.LoadScene(scene);
        }
    }
}
