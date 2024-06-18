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

        [SerializeField] int tileMin = 2;
        [SerializeField] int tileMax = 5;
        [SerializeField] int numberMin = 3;
        [SerializeField] int numberMax = 8;
        [SerializeField] int safetyLimit = 4;

        private void Awake()
        {
            tiles = transform.Find("Tiles").GetComponentsInChildren<SafetyTile>();
            minigame = GetComponent<PlayableMinigame>();
            cycle = GetComponent<DeathmatchCycle>();
        }

        public void RerollTiles()
        {
            int tileCount = Random.Range(tileMin, tileMax + 1);
            int numberTotal = Random.Range(numberMin, numberMax + 1);
            int safetyTile = 0;
            int safetyNumber = 0;
            for (int i = 0; i < tileCount - 1; ++i)
            {
                do
                    safetyTile = Random.Range(0, tiles.Length);
                while(safetyTiles.Contains(safetyTile) == true);

                safetyNumber = Random.Range(0, Mathf.Min(safetyLimit, numberTotal));
                numberTotal -= safetyNumber;
                UpdateSafetyNumberClientRpc(safetyTile, safetyNumber);
            }

            do
                safetyTile = Random.Range(0, tiles.Length);
            while (safetyTiles.Contains(safetyTile) == true);
            UpdateSafetyNumberClientRpc(safetyTile, numberTotal);

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
                if(minigame.PlayerDictionary.ContainsKey(i.clientID) == false)
                    return;

                SafetyZonePlayerController player = minigame.PlayerDictionary[i.clientID] as SafetyZonePlayerController;
                if (player?.IsDead == false && player?.IsSafety == false)
                {
                    player.Health.OnDamaged(-1f, transform, transform.position);
                    player.IsDead = true;
                    BroadcastPlayerDeadClientRpc(i.clientID);
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

        [ClientRpc]
        private void BroadcastPlayerDeadClientRpc(ulong clientID)
        {
            cycle.HandlePlayerDead(clientID);
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
