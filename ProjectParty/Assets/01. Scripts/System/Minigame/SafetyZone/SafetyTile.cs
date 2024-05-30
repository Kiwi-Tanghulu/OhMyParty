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

        private int safetyNumber = 0;

        private void Awake()
        {
            tileCollision = transform.Find("Collision").GetComponent<SafetyTileCollision>();
            tileVisual = transform.Find("Visual").GetComponent<SafetyTileVisual>();
            block = transform.Find("Block").GetComponent<SafetyTileBlock>();

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
            ToggleBlock(IsSafetyZone());
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

        private void HandlePlayerCountChanged()
        {
            ToggleBlock(IsSafetyZone());
        }
    }
}
