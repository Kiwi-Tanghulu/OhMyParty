using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTile : MonoBehaviour
    {
        private SafetyTileCollision tileCollision = null;
        private SafetyTileVisual tileVisual = null;

        private int safetyNumber = 0;

        private void Awake()
        {
            tileCollision = transform.Find("Collision").GetComponent<SafetyTileCollision>();
            tileVisual = transform.Find("Visual").GetComponent<SafetyTileVisual>();
        }

        public void SetSafetyNumber(int number)
        {
            safetyNumber = number;
            tileVisual.SetNumberText(safetyNumber);
        }

        public bool IsSafetyZone()
        {
            return tileCollision.IncludePlayerCount == safetyNumber;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
