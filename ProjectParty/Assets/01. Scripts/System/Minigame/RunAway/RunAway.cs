using OMG.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class RunAway : PlayableMinigame
    {
        [Space]
        [SerializeField] private MapPart startMapPart;
        [SerializeField] private MapPart endMapPart;
        [SerializeField] private List<MapPart> middleMapPartList;
        [SerializeField] private Transform mapPartParentTrm;
        [SerializeField] private int spawnMapPartCount;

        private RaceCycle raceCycle;

        public override void Init()
        {
            base.Init();

            raceCycle = cycle as RaceCycle;

            CreateMap();
        }

        private void CreateMap()
        {
            MapPart part = null;
            MapPart prevPart = null;

            prevPart = Instantiate(startMapPart, mapPartParentTrm);
            prevPart.SetPosition(null);

            List<MapPart> shuffledMapPart = middleMapPartList.Shuffle();
            for(int i = 0; i < spawnMapPartCount; i++)
            {
                part = Instantiate(shuffledMapPart[i], mapPartParentTrm);
                part.SetPosition(prevPart);
                prevPart = part;
            }

            Instantiate(endMapPart, mapPartParentTrm).SetPosition(prevPart);
        }
    }
}