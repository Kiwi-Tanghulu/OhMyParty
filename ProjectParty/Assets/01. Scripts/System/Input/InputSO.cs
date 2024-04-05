using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSO : ScriptableObject
{
    public InputMapType inputMapType;

    protected virtual void OnEnable()
    {
        Debug.Log($"Set InputSO : {inputMapType}");
    }
}
