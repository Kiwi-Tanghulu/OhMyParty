using OMG.Inputs;
using UnityEngine;

namespace OMG.UI.Lobbies
{
    public class DemoPanel : MonoBehaviour
    {
        public void Start()
        {
            gameObject.SetActive(false);
        }

        public void Display(bool active)
        {
            InputManager.ChangeInputMap(active ? InputMapType.UI : InputMapType.Play);
            gameObject.SetActive(active);
            if(active)
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
