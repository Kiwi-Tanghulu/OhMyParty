using Cinemachine;
using OMG.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.RunAway
{
    public class RunAway : PlayableMinigame
    {
        [Space]
        [SerializeField] private MapPart startMapPart;
        [SerializeField] private MapPart endMapPart;
        [SerializeField] private List<MapPart> middleMapPartList;
        [SerializeField] private Transform mapPartParentTrm;
        [SerializeField] private int spawnMapPartCount;

        [Space]
        [SerializeField] private RunAwayMonster monsterPrefab;
        [SerializeField] private Transform monsterSpawnPoint;
        [SerializeField] private float monsterSpawnDelay;

        private RunAwayCycle runAwayCycle;

        protected override void OnGameInit()
        {
            base.OnGameInit();

            runAwayCycle = cycle as RunAwayCycle;

            CreateMap();
        }

        private void CreateMap()
        {
            MapPart part = null;
            MapPart prevPart = null;

            prevPart = Instantiate(startMapPart, mapPartParentTrm);
            prevPart.Init(null, IsHost);

            List<MapPart> shuffledMapPart = middleMapPartList.Shuffle();
            for(int i = 0; i < spawnMapPartCount; i++)
            {
                part = Instantiate(shuffledMapPart[i], mapPartParentTrm);
                part.Init(prevPart, IsHost);
                prevPart = part;
            }

            Instantiate(endMapPart, mapPartParentTrm).Init(prevPart, IsHost);
        }

        private IEnumerator SpawnMonsterDelay()
        {
            yield return new WaitForSeconds(monsterSpawnDelay);

            Instantiate(monsterPrefab, monsterSpawnPoint.position,
                monsterSpawnPoint.rotation, transform).Init(runAwayCycle);
        }
    }
}