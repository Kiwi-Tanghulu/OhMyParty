using OMG.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class Player : MonoBehaviour
    {
        private ActioningPlayer actioningPlayer;
        public ActioningPlayer test;//tset

        private void Start()
        {
            InputManager.ChangeInputMap(InputMapType.Play);//test

            SetActioningPlayer(test);
        }

        public void SetActioningPlayer(ActioningPlayer newActioningPlayer)
        {
            actioningPlayer = Instantiate(newActioningPlayer, transform);
            actioningPlayer.InitActioningPlayer(this);
        }

        private void Update()
        {
            actioningPlayer?.UpdateActioningPlayer();
        }
    }
}
