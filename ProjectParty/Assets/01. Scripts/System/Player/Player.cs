using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class Player : MonoBehaviour
    {
        private ActioningPlayer actioningPlayer;

        public void SetActioningPlayer(ActioningPlayer newActioningPlayer)
        {
            actioningPlayer = Instantiate(newActioningPlayer, transform);
            actioningPlayer.Init();
        }
    }
}
