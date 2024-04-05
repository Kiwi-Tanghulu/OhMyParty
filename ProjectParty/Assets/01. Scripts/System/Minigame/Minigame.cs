using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames
{
    public abstract class Minigame : NetworkBehaviour
    {
        // public NetworkObject NetworkObject { get; } = null;

        public abstract void StartGame();
        public abstract void FinishGame();
    }
}
