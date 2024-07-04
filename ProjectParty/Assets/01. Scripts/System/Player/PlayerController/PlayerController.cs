using OMG.FSM;
using OMG.Lobbies;
using Steamworks;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerController : CharacterController
    {
        private PlayerVisual visual;
        public PlayerVisual Visual => visual;

        protected override void InitCompos()
        {
            visual = InitCompo(transform.Find("Visual").GetComponent<PlayerVisual>());

            base.InitCompos();
        }
    }
}