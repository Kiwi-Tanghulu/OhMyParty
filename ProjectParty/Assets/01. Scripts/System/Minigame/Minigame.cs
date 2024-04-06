using Unity.Netcode;

namespace OMG.Minigames
{
    public abstract class Minigame : NetworkBehaviour
    {
        public abstract void StartGame();
        public abstract void FinishGame();
    }
}
