using OMG.Extensions;

namespace OMG.Minigames.OhMySword
{
    public class OhMySwordCycle : TimeAttackCycle
    {
        private new PlayableMinigame minigame = null;

        protected override void Awake()
        {
            base.Awake();
            minigame = base.minigame as PlayableMinigame;
        }

        public void Respawn(ulong clientID)
        {
            ulong targetID = minigame.PlayerDatas.PickRandom(i => i.clientID != clientID).clientID;
            minigame.PlayerDictionary[clientID].GetComponent<CatchTailPlayer>().SetTargetPlayerID(targetID);

            minigame.RespawnPlayer(clientID);
        }
    }
}
