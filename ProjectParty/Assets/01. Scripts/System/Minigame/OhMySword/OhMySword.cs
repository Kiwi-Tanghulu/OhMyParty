using System.Collections.Generic;
using OMG.NetworkEvents;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class OhMySword : PlayableMinigame
    {
        [SerializeField] ScoreBoxTable[] scoreBoxTables = null;
        public LinkedList<XPObject> ScoreContainer = null;
        public List<ScoreBox> ScoreBoxes = null;

        [SerializeField] Material skyboxMaterial = null;
        private OhMySwordCycle ohMySwordCycle = null;
        protected override bool ShufflePosition => true;

        protected override void Awake()
        {
            base.Awake();
            RenderSettings.skybox = skyboxMaterial;
        }

        protected override void OnGameInit()
        {
            base.OnGameInit();

            if(IsHost == false)
                return;

            ScoreBoxes = new List<ScoreBox>();
            ScoreContainer = new LinkedList<XPObject>();
            for(int i = 0; i < scoreBoxTables.Length; ++i)
            {
                ScoreBoxTable table = scoreBoxTables[i];
                for(int j = 0; j < table.Count; ++j)
                {
                    ScoreBox scoreBox = Instantiate(table.Prefab);
                    scoreBox.NetworkObject.Spawn(true);
                    scoreBox.Init(table.SpawnPositionTable, ScoreContainer);
                    scoreBox.Respawn();
                    ScoreBoxes.Add(scoreBox);
                }
            }

            ohMySwordCycle = cycle as OhMySwordCycle;
        }

        protected override void OnGameStart()
        {
            base.OnGameStart();

            if(IsHost == false)
                return;

            ohMySwordCycle.StartCycle();
        }

        protected override void OnGameRelease()
        {
            base.OnGameRelease();

            if(IsHost == false)
                return;

            foreach(XPObject xp in ScoreContainer)
            {
                if(xp == null || xp.NetworkObject.IsSpawned == false)
                    continue;
                
                xp.NetworkObject.Despawn(true);
            }

            for(int i = 0; i < ScoreBoxes.Count; ++i)
                ScoreBoxes[i].NetworkObject.Despawn();

            ScoreBoxes.Clear();
        }
    }
}
