using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class OhMySword : PlayableMinigame
    {
        [SerializeField] ScoreBoxTable[] scoreBoxTables = null;
        private List<ScoreBox> scoreBoxes = null;

        [SerializeField] Material skyboxMaterial = null;
        private TimeAttackCycle timeAttackCycle = null;
        protected override bool ShufflePosition => true;

        #region Test

        protected override void Awake()
        {
            base.Awake();
            RenderSettings.skybox = skyboxMaterial;
            // InputManager.ChangeInputMap(InputMapType.Play);
            // // GameManager.Instance.CursorActive = false;

            // Cursor.visible = false;
            // Cursor.lockState = CursorLockMode.Locked;
        }

        #endregion

        public override void Init()
        {
            base.Init();

            if(IsHost == false)
                return;

            scoreBoxes = new List<ScoreBox>();
            for(int i = 0; i < scoreBoxTables.Length; ++i)
            {
                ScoreBoxTable table = scoreBoxTables[i];
                for(int j = 0; j < table.Count; ++j)
                {
                    ScoreBox scoreBox = Instantiate(table.Prefab);
                    scoreBox.NetworkObject.Spawn(true);
                    scoreBox.Init(table.SpawnPositionTable);
                    scoreBox.Respawn();
                    scoreBoxes.Add(scoreBox);
                }
            }

            timeAttackCycle = cycle as TimeAttackCycle;
        }

        public override void StartGame()
        {
            base.StartGame();

            if(IsHost == false)
                return;

            timeAttackCycle.StartCycle();
        }

        public override void Release()
        {
            base.Release();

            if(IsHost == false)
                return;

            scoreBoxes.ForEach(i => i.NetworkObject.Despawn());
            scoreBoxes.Clear();
        }
    }
}
