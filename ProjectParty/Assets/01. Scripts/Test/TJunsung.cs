using OMG;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TJunsung : MonoBehaviour
{
    public TMP_InputField input;

    private void Start()
    {
        input.onEndEdit.AddListener(End);
        input.onSubmit.AddListener(submit);
    }

    void End(string message)
    {
        Debug.Log("end");
    }

    void submit(string message)
    {
        Debug.Log("submit");
    }
}