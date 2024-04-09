using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Player
{
    public class Player : NetworkBehaviour
    {
        private ActioningPlayer actioningPlayer;

        public override void OnNetworkSpawn()
        {
            Debug.Log($"on network spawn player : {OwnerClientId}");
            PlayerManager.Instance.PlayerDic.Add(OwnerClientId, this);

            if (!IsOwner)
                return;

            InputManager.ChangeInputMap(InputMapType.Play);//test

            InstantiateActioningPlayerServerRpc(ActioningPlayerType.Test);
        }

        [ServerRpc]
        public void InstantiateActioningPlayerServerRpc(ActioningPlayerType type)
        {
            ActioningPlayer actioningPlayer = Instantiate(PlayerManager.Instance.GetActioningPlayer(type).Prefab, transform);
            Debug.Log($"instantiating actioning player : {OwnerClientId}");
            actioningPlayer.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
        }

        public void SetActioningPlayer(ActioningPlayer actioningPlayer)
        {
            this.actioningPlayer = actioningPlayer;
        }

        private void Update()
        {
            if (!IsOwner)
                return;
            Debug.Log($"update player : {OwnerClientId}");

            actioningPlayer?.UpdateActioningPlayer();
        }
    }
}
