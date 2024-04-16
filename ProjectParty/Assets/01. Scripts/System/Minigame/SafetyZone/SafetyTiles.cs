using OMG.Extensions;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTiles : NetworkBehaviour
    {
        [SerializeField] SafetyTile[] tiles = null;

        public void RerollTiles()
        {
            Debug.Log("Reroll");
            for(int i = 0; i < tiles.Length; ++i)
            {
                int safetyNumber = Random.Range(0, 4);
                UpdateSafetyNumberClientRpc(i, safetyNumber);
            }
        }

        public void DecisionSafetyZone()
        {
            Debug.Log("Decision");
            tiles.ForEach((i, index) => {
                if(i.IsSafetyZone())
                    return;
                
                TileActiveClientRpc(index);
            });
        }

        public void ResetTiles()
        {
            Debug.Log("Reset");
            ResetTilesClientRpc();
        }

        [ClientRpc]
        private void UpdateSafetyNumberClientRpc(int index, int number)
        {
            tiles[index].SetSafetyNumber(number);
        }

        [ClientRpc]
        private void TileActiveClientRpc(int index)
        {
            tiles[index].SetActive(false);
        }

        [ClientRpc]
        private void ResetTilesClientRpc()
        {
            tiles.ForEach(i => {
                i.SetActive(true);
                i.SetSafetyNumber(-1);
            });
        }
    }
}
