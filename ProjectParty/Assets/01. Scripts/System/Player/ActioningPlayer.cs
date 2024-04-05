using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public abstract class ActioningPlayer : MonoBehaviour
    {
        protected Player player;

        public abstract void InitActioningPlayer();

        public abstract void UpdateActioningPlayer();
    }
}
