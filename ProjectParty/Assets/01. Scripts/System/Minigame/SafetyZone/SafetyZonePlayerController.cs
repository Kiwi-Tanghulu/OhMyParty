using OMG.Player;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyZonePlayerController : PlayerController
    {
        public bool IsSafety = false;
        public bool IsDead = false;

        public PlayerHealth Health = null;

        private void Awake()
        {
            Health = GetComponent<PlayerHealth>();
        }
    }
}
