using OMG.Inputs;
using OMG.Network;
using Unity.Netcode;
using UnityEngine;

namespace OMG.UI.Lobbies
{
    public class ExitPanel : UIPanel
    {
        public void Display(bool active)
        {
            if (active)
                Show();
            else
                UIManager.Instance.HidePanel();
            //if (gameObject.activeSelf == active)
            //    return;

            //if (active)
            //    InputManager.ChangeInputMap(InputMapType.UI);
            //else
            //    InputManager.UndoChangeInputMap();

            //gameObject.SetActive(active);
        }

        public void Exit()
        {
            ClientManager_.Instance.Disconnect();
        }
    }
}
