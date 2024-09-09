using System.Collections.Generic;
using OMG.Minigames;
using OMG.Utility;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class TeamPanel : PlayerPanel
    {
        [SerializeField] protected OptOption<TeamSlot> teamSlots = new OptOption<TeamSlot>();

        protected override void Awake()
        {
            // dont query
        }

        public override void Init(Minigame minigame)
        {
            TeamMinigameCycle cycle = minigame.Cycle as TeamMinigameCycle;
            if(cycle == null)
                return;

            playerSlots = new List<PlayerSlot>(minigame.PlayerDatas.Count);

            foreach(ulong clientID in cycle.TeamInfo.Keys)
            {
                PlayerSlot slot = teamSlots[cycle.TeamInfo[clientID]].AddTeamMember(clientID);
                RenderTexture playerRenderTexture = PlayerManager.Instance.GetPlayerRenderTargetVisual(clientID).RenderTexture;

                slot.Init(playerRenderTexture);
                playerSlots.Add(slot);
            }
        }
    }
}
