#if UNITY_EDITOR
using OMG.Inputs;
using UnityEngine;

public class TJunsung : MonoBehaviour
{
    private void Start()
    {
        InputManager.ChangeInputMap(InputMapType.Play);
    }
}
#endif