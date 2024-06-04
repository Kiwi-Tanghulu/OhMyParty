using OMG;
using OMG.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TJunsung : MonoBehaviour
{
    public FeedbackPlayer p;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            p.Play(transform.position);
    }
}
