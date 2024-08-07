using OMG.Extensions;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class OhMySwordCycle : TimeAttackCycle
    {
        private PlayableMinigame playableMinigame = null;

        public override void Init(Minigame minigame)
        {
            base.Init(minigame);

            playableMinigame = minigame as PlayableMinigame;
        }

        public override void StartCycle()
        {
            base.StartCycle();
            playableMinigame.PlayerDatas.ForEach(i => SetTarget(i.clientID));
        }

        public void Respawn(ulong clientID)
        {
            SetTarget(clientID);
            playableMinigame.RespawnPlayer(clientID);
        }

        public void SetTarget(ulong clientID)
        {
            ulong targetID = clientID;
            if(minigame.PlayerDatas.Count > 1)
                targetID = playableMinigame.PlayerDatas.PickRandom(i => i.clientID != clientID).clientID;

            playableMinigame.PlayerDictionary[clientID].GetComponent<CatchTailPlayer>().SetTargetPlayerID(targetID);
        }
    }
}
