using OMG.Inputs;
using OMG.Networks;
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
            if(GuestManager.Instance.Alive)
                GuestManager.Instance.Disconnect();
            else if(HostManager.Instance.Alive)
                HostManager.Instance.Disconnect();
        }
    }
}
