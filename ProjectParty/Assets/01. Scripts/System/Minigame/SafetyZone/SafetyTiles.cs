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

        public int SafetyTileCount = 3;

        private void Awake()
        {
            tiles = transform.Find("Tiles").GetComponentsInChildren<SafetyTile>();
            minigame = GetComponent<PlayableMinigame>();
            cycle = GetComponent<DeathmatchCycle>();
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

                int safetyNumber = Random.Range(0, 2);
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

            minigame.PlayerDatas.ForEach(i => {
                SafetyZonePlayerController player = minigame.PlayerDictionary[i.clientID] as SafetyZonePlayerController;
                if (player.IsDead == false && player.IsSafety == false)
                {
                    player.Health.OnDamaged(10f, transform, transform.position);
                    player.IsDead = true;
                    cycle.HandlePlayerDead(i.clientID);
                }
            });

            DecisionSafetyZoneClientRpc();
        }

        public void ResetTiles()
        {
            Debug.Log("Reset");
            ResetTilesClientRpc();
            safetyTiles.Clear();
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
            // StartCoroutine(this.DelayCoroutine(fallingPostpone, () => groundCollider.SetActive(false)));
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
            foreach(int i in safetyTiles)
            {
                tiles[i].Reset();
                tiles[i].SetActive(false);    
            }

            // groundCollider.SetActive(true);
            onResetEvent?.Invoke();
        }
    }
}
