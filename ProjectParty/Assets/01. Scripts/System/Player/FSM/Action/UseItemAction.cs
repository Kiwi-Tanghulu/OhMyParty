using OMG.FSM;
using OMG.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player.FSM
{
    public class UseItemAction : PlayerFSMAction
    {
        [SerializeField] private PlayInputSO input;

        private PlayerItemHolder itemHolder;

        public override void Init(CharacterFSM brain)
        {
            base.Init(brain);

            itemHolder = player.GetCharacterComponent<PlayerItemHolder>();
        }

        public override void EnterState()
        {
            base.EnterState();

            input.OnInteractEvent += UseItem;
        }

        public override void ExitState()
        {
            base.ExitState();

            input.OnInteractEvent -= UseItem;
        }

        private void UseItem(bool started)
        {
            if (started)
            {
                itemHolder.UseItem();
            }
        }
    }
}