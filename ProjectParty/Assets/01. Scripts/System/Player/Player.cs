using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class Player : MonoBehaviour
    {
        private ActioningPlayer actioningPlayer;
        public TestActioningPlayer test;//tset

        private void Start()
        {
            InputManager.ChangeInputMap(InputMapType.Play);//test

            SetActioningPlayer(test);
        }

        public void SetActioningPlayer(ActioningPlayer newActioningPlayer)
        {
            actioningPlayer = Instantiate(newActioningPlayer, transform);
            actioningPlayer.InitActioningPlayer();
        }

        private void Update()
        {
            actioningPlayer.UpdateActioningPlayer();
        }
    }
}
