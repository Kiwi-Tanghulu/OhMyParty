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
        }

        public bool IsSafetyZone()
        {
            return includePlayers.Count == safetyNumber;
        }

        public void ActiveTile()
        {
            gameObject.SetActive(true);

            tileVisual.Appear();
            SetSafety(IsSafetyZone());
        }

        public void InactiveTile()
        {
            SetSafety(false);
            tileVisual.Disappear(() => gameObject.SetActive(false));
        }

        public void Init()
        {
            Reset();
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            safetyNumber = 100;
            tileVisual.SetNumberText(-1);
            includePlayers.Clear();
        }

        private void SetSafety(bool safety)
        {
            foreach(SafetyZonePlayerController p in includePlayers)
                p.IsSafety = safety;
            if(safety)
                Debug.Log(gameObject.activeInHierarchy);
            block.SetActive(safety);
        }

        private void HandlePlayerEnter(SafetyZonePlayerController player)
        {
            includePlayers.Add(player);

            bool isSafety = IsSafetyZone();
            player.IsSafety = isSafety;
            SetSafety(isSafety);
        }

        private void HandlePlayerExit(SafetyZonePlayerController player)
        {
            includePlayers.Remove(player);
            player.IsSafety = false;
            
            bool isSafety = IsSafetyZone();
            SetSafety(isSafety);
        }
    }
}
