using System.Collections.Generic;
using OMG.Inputs;
using UnityEngine;

namespace OMG.Minigames.PunchClub
{
    public class PunchClub : PlayableMinigame
    {
        [SerializeField] List<HitableObject> hitableObjects = new List<HitableObject>();

        private DeathmatchCycle deathmatchCycle = null;
        protected override bool ShufflePosition => true;

        #region Test

        protected override void Awake()
        {
            base.Awake();
            InputManager.ChangeInputMap(InputMapType.Play);
        }

        #endregion

        public override void Init()
        {
            base.Init();

            hitableObjects.ForEach(i => i.Init(NetworkObject));

            if(IsHost == false)
                return;

            deathmatchCycle = cycle as DeathmatchCycle;
        }

        public override void StartGame()
        {
            base.StartGame();
            Debug.Log("game start");
        }
    }
}
