using System;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Test
{
    public class TIntro : MonoBehaviour
    {
        public void Lobby()
        {
            NetworkManager.Singleton.ConnectionApprovalCallback += HandleConnectionApproval;
            if(NetworkManager.Singleton.StartHost())
                SceneManager.Instance.LoadScene(SceneType.LobbyScene);
        }

        public void Join()
        {
            NetworkManager.Singleton.StartClient();
        }

        private void HandleConnectionApproval(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
        {
            response.Approved = true;
            response.CreatePlayerObject = false;
        }
    }
}
