using OMG.FSM;
using Steamworks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class TPSTurnAction : PlayerFSMAction
    {
        private TPSPlayerCamera cam;

        [SerializeField] private bool showCameraForwardImmediately;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            cam = player.GetCharacterComponent<TPSPlayerCamera>();
        }

        public override void UpdateState()
        {
            base.UpdateState();

            if (showCameraForwardImmediately)
            {
                if (cam != null)
                {
                    player.transform.rotation = cam.Forward;
                }
            }
        }
    }
}