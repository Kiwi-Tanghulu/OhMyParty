using System.Collections.Generic;
using OMG.Extensions;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTiles : NetworkBehaviour
    {
        [SerializeField] UnityEvent onRerollEvent = null;
        [SerializeField] UnityEvent onDecisionEvent = null;
        [SerializeField] UnityEvent onResetEvent = null;

        private DeathmatchCycle cycle = null;
        private PlayableMinigame minigame = null;

        private SafetyTile[] tiles = null;
        private HashSet<int> safetyTiles = new HashSet<int>();

        public int SafetyTileCount = 1;

        private void Awake()
        {
            tiles = transform.Find("Tiles").GetComponentsInChildren<SafetyTile>();
            minigame = GetComponent<PlayableMinigame>();
            cycle = GetComponent<DeathmatchCycle>();
        }

        public void RerollTiles()
        {
            for(int i = 0; i < SafetyTileCount; ++i)
            {
                int safetyTile;
                do
                    safetyTile = Random.Range(0, tiles.Length);
                while(safetyTiles.Contains(safetyTile) == true);

                int safetyNumber = Random.Range(0, 2);
                UpdateSafetyNumberClientRpc(safetyTile, safetyNumber);
            }

            RerollTilesClientRpc();
        }

        public void DecisionSafetyZone()
        {
            foreach(int i in safetyTiles)
            {
                SafetyTile tile = tiles[i];
                if(tile.IsSafetyZone())
                    continue;
                TileInactiveClientRpc(i);
            }

            minigame.PlayerDatas.ForEach(i => {
                SafetyZonePlayerController player = minigame.PlayerDictionary[i.clientID] as SafetyZonePlayerController;
                if (player.IsDead == false && player.IsSafety == false)
                {
                    player.Health.OnDamaged(-1f, transform, transform.position);
                    player.IsDead = true;
                    cycle.HandlePlayerDead(i.clientID);
                }
            });

            DecisionSafetyZoneClientRpc();
        }

        public void ResetTiles()
        {
            ResetTilesClientRpc();
            safetyTiles.Clear();
        }

        public void Init()
        {
            InitClientRpc();
        }

        #region Cycle
        [ClientRpc]
        public void InitClientRpc()
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
            onDecisionEvent?.Invoke();
        }

        [ClientRpc]
        private void ResetTilesClientRpc()
        {
            foreach(int i in safetyTiles)
                InactiveTile(i);

            onResetEvent?.Invoke();
        }
        #endregion

        [ClientRpc]
        private void UpdateSafetyNumberClientRpc(int index, int number)
        {
            safetyTiles.Add(index);

            tiles[index].SetSafetyNumber(number);
            tiles[index].ActiveTile();
        }

        [ClientRpc]
        private void TileInactiveClientRpc(int index)
        {
            InactiveTile(index);
        }

        private void InactiveTile(int index)
        {
            tiles[index].InactiveTile();
            tiles[index].Reset();
        }
    }
}
