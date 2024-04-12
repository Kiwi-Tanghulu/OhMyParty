using OMG.Lobbies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Players
{
    public class SitState : FSMState
    {
        private MinigameSpot minigameSpot;

        public override void InitState(PlayerController actioningPlayer)
        {
            base.InitState(actioningPlayer);

            minigameSpot = GameObject.FindObjectOfType<MinigameSpot>();
        }

        protected override void OwnerEnterState()
        {
            base.OwnerEnterState();

            Transform sitPoint = minigameSpot.SitPointList[(int)actioningPlayer.OwnerClientId];
            actioningPlayer.transform.position = sitPoint.position;
            actioningPlayer.transform.rotation = sitPoint.rotation;
        }
    }
}