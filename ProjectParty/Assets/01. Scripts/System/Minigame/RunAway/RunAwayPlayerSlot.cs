using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class RunAwayPlayerSlot : RacePlayerSlot
    {
        [SerializeField] private GameObject deadCheck = null;

        public override void Init(RenderTexture playerIcon)
        {
            base.Init(playerIcon);
            SetDead(false);
        }

        public void SetDead(bool dead)
        {
            deadCheck.SetActive(dead);
        }
    }
}
