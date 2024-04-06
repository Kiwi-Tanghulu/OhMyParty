using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Lobbies
{
    public class Lobby : NetworkBehaviour
    {
        private Dictionary<Type, LobbyComponent> lobbyComponents = null;

        private void Awake()
        {
            LobbyComponent[] components = GetComponents<LobbyComponent>();
            components.ForEach(i => i.Init(this));
            lobbyComponents = components.GetTypeDictionary();
        }

        public T GetLobbyComponent<T>() where T : LobbyComponent => lobbyComponents[typeof(T)] as T;
    }
}
