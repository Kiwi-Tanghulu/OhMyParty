using OMG.Inputs;
using OMG.Minigames;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventure : PlayableMinigame
    {
        [SerializeField] TaggerMoveTargetSO tagger_mSO;
        [SerializeField] Material mazeAdventureSkyBoxMaterial;
        [SerializeField] List<Transform> taggerMoveTrmList;
        private DeathmatchCycle deathmatchCycle = null;
        private TaggerSpawner taggerSpawner = null;
        private ItemSpawner itemSpawner = null;
        [SerializeField] private MazeAdventureMapManager mapManager;
        public MazeAdventureMapManager MapManager => mapManager;
        public override void Init(params ulong[] playerIDs)
        {
            base.Init(playerIDs);

            deathmatchCycle = cycle as DeathmatchCycle;

            taggerSpawner = GetComponent<TaggerSpawner>();
            itemSpawner = GetComponent<ItemSpawner>();

            StartIntro();
            SettingTaggerMoveTargetSO();
        }

        protected override void Awake()
        {
            base.Awake();
            RenderSettings.skybox = mazeAdventureSkyBoxMaterial;
        }

        public override void StartGame()
        {
            base.StartGame();
            InputManager.ChangeInputMap(InputMapType.Play);
            taggerSpawner.enabled = false;
            itemSpawner.enabled = false;

            if (!IsHost) return;

            taggerSpawner.enabled = true;
            itemSpawner.enabled = true;
            taggerSpawner.StartSpawn();
            itemSpawner.StartSpawn();
        }

        private void SettingTaggerMoveTargetSO()
        {
            tagger_mSO.moveTargetList = taggerMoveTrmList.Select(t => t.position).ToList();
        }

        public override void Release()
        {
            base.Release();
        }
    }

}
