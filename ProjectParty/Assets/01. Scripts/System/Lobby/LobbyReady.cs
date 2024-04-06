using Unity.Netcode;
using UnityEngine;

namespace OMG.Lobbies
{
    public class LobbyReady : LobbyComponent
    {
        [ServerRpc]
        public void ReadyServerRpc()
        {

        }
    }
}
