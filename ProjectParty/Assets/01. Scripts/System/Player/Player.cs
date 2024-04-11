using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Players
{
    public class Player : NetworkBehaviour
    {
        [SerializeField] ActioningPlayerType playerType;
        private ActioningPlayer actioningPlayer;

        public override void OnNetworkSpawn()
        {
            PlayerManager.Instance.PlayerDic[OwnerClientId] = this;

            if (!IsOwner)
                return;

            InputManager.ChangeInputMap(InputMapType.Play);//test
            InstantiateActioningPlayerServerRpc(playerType);
        }

        [ServerRpc]
        public void InstantiateActioningPlayerServerRpc(ActioningPlayerType type)
        {
            ActioningPlayer actioningPlayer = Instantiate(PlayerManager.Instance.GetActioningPlayer(type).Prefab, transform);
            actioningPlayer.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
        }

        public void SetActioningPlayer(ActioningPlayer actioningPlayer)
        {
            this.actioningPlayer = actioningPlayer;
        }

        private void Update()
        {
            actioningPlayer?.UpdateActioningPlayer();
        }
    }
}
