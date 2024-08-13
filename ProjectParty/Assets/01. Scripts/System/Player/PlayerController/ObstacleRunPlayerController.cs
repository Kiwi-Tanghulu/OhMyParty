using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Player
{
    public class ObstacleRunPlayerController : PlayerController
    {
        private Transform respawnPoint;

        public void SetRespawnPoint(Transform point)
        {
            respawnPoint = point;
        }

        public override void Respawn(Vector3 pos, Vector3 rot)
        {
            Respawn();
        }

        private void Respawn()
        {
            base.Respawn(respawnPoint.position, respawnPoint.eulerAngles);
        }
    }
}
