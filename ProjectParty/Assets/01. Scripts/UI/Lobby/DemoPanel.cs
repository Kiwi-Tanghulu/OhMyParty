using OMG.Inputs;
using UnityEngine;

namespace OMG.UI.Lobbies
{
    public class DemoPanel : MonoBehaviour
    {
        public void Display(bool active)
        {
            InputManager.ChangeInputMap(active ? InputMapType.UI : InputMapType.Play);
            gameObject.SetActive(active);
        }
    }
}
