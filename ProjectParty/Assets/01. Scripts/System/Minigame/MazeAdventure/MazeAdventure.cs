using OMG.Input;
using OMG.Minigames;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventure : PlayableMinigame
    {
        [SerializeField] TaggerMoveTargetSO tagger_mSO;
        [SerializeField] Material mazeAdventureSkyBoxMaterial;
        [SerializeField] List<Transform> taggerMoveTrmList;
        private DeathmatchCycle deathmatchCycle = null;
        private TaggerSpawner spawner = null;
        public override void Init(params ulong[] playerIDs)
        {
            base.Init(playerIDs);

            for (int i = 0; i < playerDatas.Count; ++i)
                CreatePlayer(i);

            deathmatchCycle = cycle as DeathmatchCycle;

            spawner = GetComponent<TaggerSpawner>();

            StartIntro();
            SettingTaggerMoveTargetSO();

            RenderSettings.skybox = mazeAdventureSkyBoxMaterial;
        }

        public override void StartGame()
        {
            base.StartGame();

            if (!IsHost) return;
            spawner.enabled = true;
            spawner.StartSpawn();

            InputManager.ChangeInputMap(InputMapType.Play);
        }

        private void SettingTaggerMoveTargetSO()
        {
            tagger_mSO.moveTargetList = taggerMoveTrmList.Select(t => t.position).ToList();
        }
    }

}
