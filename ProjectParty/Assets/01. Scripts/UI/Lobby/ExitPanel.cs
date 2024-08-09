using OMG.Inputs;
using OMG.Network;
using Unity.Netcode;
using UnityEngine;

namespace OMG.UI.Lobbies
{
    public class ExitPanel : MonoBehaviour
    {
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void Display(bool active)
        {
            if (gameObject.activeSelf == active)
                return;

            if (active)
                InputManager.ChangeInputMap(InputMapType.UI);
            else
                InputManager.UndoChangeInputMap();

            gameObject.SetActive(active);
        }

        public void Exit()
        {
            ClientManager.Instance.Disconnect();
        }
    }
}
