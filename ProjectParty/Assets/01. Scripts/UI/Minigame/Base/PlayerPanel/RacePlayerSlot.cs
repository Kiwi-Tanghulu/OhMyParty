using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class RacePlayerSlot : PlayerSlot
    {
        [SerializeField] private GameObject goalCheck = null;

        public override void Init(RenderTexture playerIcon)
        {
            base.Init(playerIcon);
            SetGoal(false);
        }

        public void SetGoal(bool goal)
        {
            goalCheck.SetActive(goal);
        }
    }
}
