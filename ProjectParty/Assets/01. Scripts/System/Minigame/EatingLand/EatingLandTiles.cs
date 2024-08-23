using OMG.Extensions;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


namespace OMG.Minigames.EatingLand
{
    public class EatingLandTiles : NetworkBehaviour
    {
        private EatingLandTile[] tiles;
        private Minigame minigame = null;
        private void Awake()
        {
            tiles = transform.Find("Tiles").GetComponentsInChildren<EatingLandTile>();

            for(int i = 0; i  < tiles.Length; i++)
            {
                tiles[i].Init(i);
                tiles[i].OnUpdateTileEvent += UpdateTileServerRPC;
            }
        }

        public void Init()
        {
            minigame = MinigameManager.Instance.CurrentMinigame;
        }

        [ServerRpc]
        private void UpdateTileServerRPC(int tileID, ulong currentOwnerId ,ulong nextOwnerId)
        {
            if(currentOwnerId != ulong.MaxValue)
            {
                minigame.PlayerDatas.ChangeData(i => i.clientID == currentOwnerId, data =>
                {
                    data.score = data.score - 1;
                    return data;
                });
            }

            minigame.PlayerDatas.ChangeData(i => i.clientID == nextOwnerId, data =>
            {
                data.score = data.score + 1;
                return data;
            });

            UpdateTileClientRPC(tileID, nextOwnerId);
        }

        [ClientRpc]
        private void UpdateTileClientRPC(int tileID, ulong nextOwnerId)
        {
            tiles[tileID].UpdateTile(nextOwnerId);
        }
    }
}
