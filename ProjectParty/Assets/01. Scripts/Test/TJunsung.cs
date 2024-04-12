using OMG;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TJunsung : MonoBehaviour
{
    public string scenename;
    
    public void SceneLoad()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback += HandleConnectionApproval;
        if (NetworkManager.Singleton.StartHost())
            OMG.SceneManager.Instance.LoadScene(scenename);
    }

    private void HandleConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        response.Approved = true;
        response.CreatePlayerObject = false;
    }
}
