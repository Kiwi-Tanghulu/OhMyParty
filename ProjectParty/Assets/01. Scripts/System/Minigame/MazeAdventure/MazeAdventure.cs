using OMG.Inputs;
using OMG.Minigames;
using OMG.NetworkEvents;
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
        
        protected override void OnGameInit()
        {
            base.OnGameInit();

            if(IsHost == false)
                return;

            deathmatchCycle = cycle as DeathmatchCycle;

            taggerSpawner = GetComponent<TaggerSpawner>();
            itemSpawner = GetComponent<ItemSpawner>();

            SettingTaggerMoveTargetSO();
        }

        protected override void Awake()
        {
            base.Awake();
            RenderSettings.skybox = mazeAdventureSkyBoxMaterial;
        }

        protected override void OnGameStart()
        {
            base.OnGameStart();

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
    }

}
