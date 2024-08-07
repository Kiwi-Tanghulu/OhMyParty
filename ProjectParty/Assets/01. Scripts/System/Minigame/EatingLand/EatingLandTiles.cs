using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


namespace OMG.Minigames.EatingLand
{
    public class EatingLandTiles : NetworkBehaviour
    {
        private EatingLandTile[] tiles;
        private void Awake()
        {
            tiles = transform.Find("Tiles").GetComponentsInChildren<EatingLandTile>();

            for(int i = 0; i  < tiles.Length; i++)
            {
                tiles[i].Init(i);
                tiles[i].OnUpdateTileEvent += UpdateTileServerRPC;
            }
        }

        [ServerRpc]
        private void UpdateTileServerRPC(int tileID, int nextIndex)
        {
            UpdateTileClientRPC(tileID, nextIndex);
        }

        [ClientRpc]
        private void UpdateTileClientRPC(int tileID, int nextIndex)
        {
            tiles[tileID].UpdateTile(nextIndex);
        }
    }
}
