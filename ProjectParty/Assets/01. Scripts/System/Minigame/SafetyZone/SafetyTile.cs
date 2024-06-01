using System.Collections.Generic;
using OMG.Tweens;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTile : MonoBehaviour
    {
        private SafetyTileCollision tileCollision = null;
        private SafetyTileVisual tileVisual = null;
        private SafetyTileBlock block = null;

        private HashSet<SafetyZonePlayerController> includePlayers = null;

        private int safetyNumber = 0;

        private void Awake()
        {
            tileCollision = transform.Find("Collision").GetComponent<SafetyTileCollision>();
            tileVisual = transform.Find("Visual").GetComponent<SafetyTileVisual>();
            block = transform.Find("Block").GetComponent<SafetyTileBlock>();

            includePlayers = new HashSet<SafetyZonePlayerController>();
            tileCollision.OnPlayerEnterEvent += HandlePlayerEnter;
            tileCollision.OnPlayerExitEvent += HandlePlayerExit;
        }

        public void SetSafetyNumber(int number)
        {
            safetyNumber = number;
            tileVisual.SetNumberText(safetyNumber);
            ToggleBlock(IsSafetyZone(includePlayers.Count));
        }

        public bool IsSafetyZone(int playerCount)
        {
            return playerCount == safetyNumber;
        }

        public void SetActive(bool active)
        {
            SetSafety(IsSafetyZone(includePlayers.Count));
            includePlayers.Clear();

            ToggleBlock(IsSafetyZone(includePlayers.Count));
            gameObject.SetActive(active);
        }

        public void ToggleBlock(bool active)
        {
            block.SetActive(active);
        }

        public void Init()
        {
            Reset();
        }

        public void Reset()
        {
            safetyNumber = 100;
            tileVisual.SetNumberText(-1);

            ToggleBlock(false);
            SetActive(false);
        }

        private void SetSafety(bool safety)
        {
            foreach(SafetyZonePlayerController p in includePlayers)
                p.IsSafety = safety;
            ToggleBlock(safety);
        }

        private void HandlePlayerEnter(SafetyZonePlayerController player)
        {
            includePlayers.Add(player);

            bool isSafety = IsSafetyZone(includePlayers.Count);
            SetSafety(isSafety);
        }

        private void HandlePlayerExit(SafetyZonePlayerController player)
        {
            bool isSafety = IsSafetyZone(includePlayers.Count - 1);
            SetSafety(isSafety);

            includePlayers.Remove(player);
        }
    }
}
