using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class RandomWayGimmick : Gimmick
    {
        [SerializeField] private List<GameObject> wayBlockers;

        private void Start()
        {
            Execute();
        }

        protected override void Execute()
        {
            base.Execute();

            int randomIndex = Random.Range(0, wayBlockers.Count);
            for(int i = 0; i <  wayBlockers.Count; i++)
            {
                if(i != randomIndex)
                    wayBlockers[i].SetActive(false);
            }
        }

        protected override bool IsExecutable()
        {
            return true;
        }
    }

}