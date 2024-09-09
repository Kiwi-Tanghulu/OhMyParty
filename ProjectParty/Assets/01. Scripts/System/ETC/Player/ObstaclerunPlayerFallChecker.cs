using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclerunPlayerFallChecker : PlayerFallChecker
{
    protected override void FallCheck(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<ObstacleRunPlayerController>(out ObstacleRunPlayerController player))
            {
                player.Respawn();
            }
        }
    }
}
