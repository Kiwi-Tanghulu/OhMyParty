using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Items
{
    public class PlayerItem : NetworkItem
    {
        protected PlayerController ownerPlayer;

        public virtual void SetOwnerPlayer(PlayerController ownerPlayer)
        {
            this.ownerPlayer = ownerPlayer;
        }

        public override void OnActive()
        {
            
        }
    }
}
