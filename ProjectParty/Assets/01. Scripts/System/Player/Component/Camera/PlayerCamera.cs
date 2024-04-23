using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerCamera : NetworkBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera camPrefab;
        protected CinemachineVirtualCamera cam;

        [SerializeField] private bool setFollow;
        [SerializeField] private bool setLook;

        protected virtual void Start()
        {
            if (!IsOwner)
                return;

            cam = Instantiate(camPrefab);

            if (setFollow)
                cam.Follow = transform;
            if (setLook)
                cam.LookAt = transform;
        }
    }
}