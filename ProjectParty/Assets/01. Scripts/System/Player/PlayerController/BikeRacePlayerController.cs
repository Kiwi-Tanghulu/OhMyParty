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
        [SerializeField] private CinemachineVirtualCamera camPrefab;
        private CinemachineVirtualCamera cam;

        public event Action OnContectGround;

        private void Start()
        {
            cam = Instantiate(camPrefab);
            cam.Follow = transform;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.CompareTag("Ground"))
            {
                OnContectGround?.Invoke();
            }
        }
    }
}