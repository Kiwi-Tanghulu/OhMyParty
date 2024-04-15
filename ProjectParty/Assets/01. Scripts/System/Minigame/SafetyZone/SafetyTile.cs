using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTile : NetworkBehaviour
    {
        private SafetyTileCollision tileCollision = null;
        private SafetyTileVisual tileVisual = null;

        private void Awake()
        {
            tileCollision = transform.Find("Collision").GetComponent<SafetyTileCollision>();
            tileVisual = transform.Find("Visual").GetComponent<SafetyTileVisual>();
        }
    }
}
