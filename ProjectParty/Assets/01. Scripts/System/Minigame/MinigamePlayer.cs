using OMG.Players;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames
{
    public class MinigamePlayer : NetworkBehaviour
    {
        private RockFestival.RockFestival minigame = null;
        public PlayerController PlayerController { get; private set; }

        private void Awake()
        {
            PlayerController = GetComponent<PlayerController>();
        }
    }
}
