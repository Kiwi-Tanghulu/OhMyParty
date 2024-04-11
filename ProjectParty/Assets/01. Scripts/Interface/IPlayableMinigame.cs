using OMG.Players;

namespace OMG.Minigames
{
    public interface IPlayableMinigame
    {
        public PlayerController[] Players { get; }
    }
}
