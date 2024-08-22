using OMG.Inputs;
using UnityEngine;

namespace OMG.UI.Lobbies
{
    public class DemoPanel : UIPanel
    {
        protected override void Start()
        {
            
        }

        public void Display(bool active)
        {
            if (active)
                Show();
            else
                UIManager.Instance.HidePanel();
            //InputManager.ChangeInputMap(active ? InputMapType.UI : InputMapType.Play);
            //gameObject.SetActive(active);
            //if(active)
            //{
            //    Cursor.lockState = CursorLockMode.None;
            //}
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
