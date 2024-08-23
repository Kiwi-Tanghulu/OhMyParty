using UnityEngine;
using OMG.Networks;

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

        private void OnApplicationQuit()
        {
            if(HostManager.Instance.Alive)
                HostManager.Instance.Disconnect();

            if(GuestManager.Instance.Alive)
                GuestManager.Instance.Disconnect();
        }

        private void InitSingleton()
        {
            SceneManager.Instance = gameObject.AddComponent<SceneManager>();
            CameraManager.CreateSingleton(gameObject);
        }

        public void InitNetwork(HostManager hostManager, GuestManager guestManager)
        {
            ClientManager.Instance = new ClientManager();
            
            hostManager.Init();
            guestManager.Init();

            ClientManager.Instance.OnDisconnectedEvent += HandleDisconnect;
        }

        private void HandleDisconnect()
        {
            SceneType scene = SceneType.IntroScene;
            SceneManager.Instance?.LoadScene(scene);
        }
    }
}
