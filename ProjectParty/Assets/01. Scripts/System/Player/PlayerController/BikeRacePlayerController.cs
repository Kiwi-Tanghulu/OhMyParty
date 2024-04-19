using Cinemachine;
using OMG.Input;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class BikeRacePlayerController : PlayerController
    {
        public event Action OnContectGround;

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.CompareTag("Ground"))
            {
                //OnContectGround?.Invoke();
                Debug.Log("on contect ground");
            }
        }
    }
}