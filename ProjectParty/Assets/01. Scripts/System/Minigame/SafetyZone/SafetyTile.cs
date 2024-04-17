using System;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTile : MonoBehaviour
    {
        private SafetyTileCollision tileCollision = null;
        private SafetyTileVisual tileVisual = null;
        private GameObject block = null;

        private int safetyNumber = 0;

        private void Awake()
        {
            tileCollision = transform.Find("Collision").GetComponent<SafetyTileCollision>();
            tileVisual = transform.Find("Visual").GetComponent<SafetyTileVisual>();
            block = transform.Find("Block").gameObject;

            tileCollision.OnPlayerCountChangedEvent += HandlePlayerCountChanged;
        }

        public void SetSafetyNumber(int number)
        {
            safetyNumber = number;
            tileVisual.SetNumberText(safetyNumber);
            ToggleBlock(IsSafetyZone());
        }

        public bool IsSafetyZone()
        {
            return tileCollision.IncludePlayerCount == safetyNumber;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        public void ToggleBlock(bool active)
        {
            block.SetActive(active);
        }

        public void Reset()
        {
            SetSafetyNumber(-1);
            ToggleBlock(false);
            SetActive(true);
        }

        private void HandlePlayerCountChanged()
        {
            ToggleBlock(IsSafetyZone());
        }
    }
}
