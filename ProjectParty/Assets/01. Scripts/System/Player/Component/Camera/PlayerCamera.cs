using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera camPrefab;
        protected CinemachineVirtualCamera cam;

        [SerializeField] private bool setFollow;
        [SerializeField] private bool setLook;

        protected virtual void Start()
        {
            cam = Instantiate(camPrefab);

            if (setFollow)
                cam.Follow = transform;
            if (setLook)
                cam.LookAt = transform;
        }
    }
}