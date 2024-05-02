using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventure : PlayableMinigame
    {
        [SerializeField] Material mazeAdventureSkyBoxMaterial;
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

            RenderSettings.skybox = mazeAdventureSkyBoxMaterial;
        }

        public override void StartGame()
        {
            base.StartGame();

            if (!IsHost) return;
            spawner.enabled = true;
            spawner.TestSpawn();
        }
    }

}
