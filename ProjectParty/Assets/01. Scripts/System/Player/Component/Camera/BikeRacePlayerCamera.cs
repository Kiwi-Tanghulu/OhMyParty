using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class BikeRacePlayerCamera : PlayerCamera
    {
        [SerializeField] private Vector3 offset;

        private void Update()
        {
            if (!IsSpawned)
                return;

            cam.transform.position = transform.position + offset;
        }
    }
}