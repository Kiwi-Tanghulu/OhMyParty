using System.Collections.Generic;
using OMG.NetworkEvents;
using UnityEngine;

namespace OMG.Minigames.PunchClub
{
    public class PunchClub : PlayableMinigame
    {
        [SerializeField] DeadZone deadZone = null;
        [SerializeField] List<HitableObject> hitableObjects = new List<HitableObject>();

        private DeathmatchCycle deathmatchCycle = null;
        protected override bool ShufflePosition => true;

        public override void Init()
        {
            base.Init();

            hitableObjects.ForEach(i => i.Init(NetworkObject));

            if(IsHost == false)
                return;

            deathmatchCycle = cycle as DeathmatchCycle;
        }

        protected override void OnGameStart(NoneParams ignore)
        {
            base.OnGameStart(ignore);

            if(IsHost)
                deadZone?.SetActive(true);
        }

        protected override void OnGameFinish(NoneParams ignore)
        {
            if(IsHost)
                deadZone?.SetActive(false);

            base.OnGameFinish(ignore);
        }
    }
}
