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

        private ExtendedAnimator animator;
        public ExtendedAnimator Animator => animator;

        protected override bool Init()
        {
            bool result = base.Init();

            if(result)
            {
                visual = transform.Find("Visual").GetComponent<PlayerVisual>();
                animator = visual.GetComponent<ExtendedAnimator>();
            }

            return result;
        }
    }
}