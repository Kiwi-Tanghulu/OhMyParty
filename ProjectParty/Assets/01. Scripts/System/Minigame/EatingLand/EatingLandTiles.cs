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
        private void UpdateTileServerRPC(int tileID, int currentIndex ,int nextIndex)
        {
            minigame.PlayerDatas.ChangeData((data) =>
            {
                if(minigame.PlayerDatas.IndexOf(data) == nextIndex)
            })
            UpdateTileClientRPC(tileID, nextIndex);
        }

        [ClientRpc]
        private void UpdateTileClientRPC(int tileID, int nextIndex)
        {
            tiles[tileID].UpdateTile(nextIndex);
        }
    }
}
