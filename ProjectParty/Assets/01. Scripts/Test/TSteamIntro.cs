using OMG.Network;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Test
{
    public class TSteamIntro : MonoBehaviour
    {
        public void Lobby()
        {
            HostManager.Instance.StartHost(4, () => {
                SceneManager.Instance.LoadScene(SceneType.LobbyScene);
            });
        }
    }
}