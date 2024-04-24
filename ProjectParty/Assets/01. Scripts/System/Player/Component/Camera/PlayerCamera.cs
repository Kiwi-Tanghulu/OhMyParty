using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using OMG;

namespace OMG.Player
{
    public class PlayerCamera : NetworkBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera camPrefab;
        protected CinemachineVirtualCamera cam;

        [SerializeField] private bool setFollow;
        [SerializeField] private bool setLook;

        public override void OnNetworkSpawn()
        {
          
            cam = Instantiate(camPrefab);

            if (IsOwner)
                cam.Priority = DEFINE.FOCUSED_PRIORITY;
            else
                cam.Priority = DEFINE.UNFOCUSED_PRIORITY;

            if (setFollow)
                cam.Follow = transform;
            if (setLook)
                cam.LookAt = transform;
        }
    }
}