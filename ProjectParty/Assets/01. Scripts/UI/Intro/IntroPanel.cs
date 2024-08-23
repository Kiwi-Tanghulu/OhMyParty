using OMG.Networks;
using UnityEngine;

namespace OMG.UI
{
    public class IntroPanel : MonoBehaviour
    {
        public void HandleMyRoom()
        {
            HostManager.Instance.StartHost(4, () => {
                SceneManager.Instance.LoadScene(SceneType.LobbyScene);
            });
        }
    }
}
