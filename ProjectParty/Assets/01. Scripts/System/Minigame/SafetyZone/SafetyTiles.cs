using System.Collections.Generic;
using OMG.Extensions;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTiles : NetworkBehaviour
    {
        [SerializeField] float fallingPostpone = 2f;
        
        [Space(15f)]
        [SerializeField] UnityEvent onRerollEvent = null;
        [SerializeField] UnityEvent onDecisionEvent = null;
        [SerializeField] UnityEvent onResetEvent = null;

        private SafetyTile[] tiles = null;
        private HashSet<int> safetyTiles = new HashSet<int>();

        public int SafetyTileCount = 3;
        private GameObject groundCollider = null;

        private void Awake()
        {
            groundCollider = transform.Find("GroundCollider").gameObject;
            tiles = transform.Find("Tiles").GetComponentsInChildren<SafetyTile>();
        }

        public void RerollTiles()
        {
            Debug.Log("Reroll");
            for(int i = 0; i < SafetyTileCount; ++i)
            {
                int safetyTile;
                do
                    safetyTile = Random.Range(0, tiles.Length);
                while(safetyTiles.Contains(safetyTile) == true);

                int safetyNumber = Random.Range(0, 4);
                safetyTiles.Add(safetyTile);
                UpdateSafetyNumberClientRpc(safetyTile, safetyNumber);
            }

            RerollTilesClientRpc();
        }

        public void DecisionSafetyZone()
        {
            Debug.Log("Decision");
            foreach(int i in safetyTiles)
            {
                SafetyTile tile = tiles[i];
                if(tile.IsSafetyZone())
                    continue;
                TileActiveClientRpc(i);
            }

            DecisionSafetyZoneClientRpc();
        }

        public void ResetTiles()
        {
            Debug.Log("Reset");
            safetyTiles.Clear();
            ResetTilesClientRpc();
        }

        public void Init()
        {
            tiles.ForEach(i => i.Init());
        }

        [ClientRpc]
        private void RerollTilesClientRpc()
        {
            onRerollEvent?.Invoke();
        }

        [ClientRpc]
        private void DecisionSafetyZoneClientRpc()
        {
            StartCoroutine(this.DelayCoroutine(fallingPostpone, () => groundCollider.SetActive(false)));
            onDecisionEvent?.Invoke();
        }

        [ClientRpc]
        private void UpdateSafetyNumberClientRpc(int index, int number)
        {
            tiles[index].SetSafetyNumber(number);
            tiles[index].SetActive(true);
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
                i.Reset();
                i.gameObject.SetActive(false);
            });
            groundCollider.SetActive(true);
            onResetEvent?.Invoke();
        }
    }
}
