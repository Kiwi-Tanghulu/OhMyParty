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
            ToggleBlock(IsSafetyZone());
        }

        public bool IsSafetyZone()
        {
            return includePlayers.Count == safetyNumber;
        }

        public void SetActive(bool active)
        {
            SetSafety(IsSafetyZone());
            includePlayers.Clear();

            ToggleBlock(IsSafetyZone());

            if(active)
            {
                gameObject.SetActive(active);
                tileVisual.Appear();
            }
            else
                gameObject.SetActive(false);
                // tileVisual.Disappear(() => gameObject.SetActive(active));
        }

        public void ToggleBlock(bool active)
        {
            block.SetActive(active);
        }

        public void Init()
        {
            safetyNumber = 100;
            tileVisual.SetNumberText(-1);

            ToggleBlock(false);
            gameObject.SetActive(false);
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
