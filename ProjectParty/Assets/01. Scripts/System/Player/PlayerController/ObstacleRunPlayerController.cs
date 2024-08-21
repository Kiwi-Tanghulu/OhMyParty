using OMG.FSM;
using OMG.Minigames;
using OMG.Player.FSM;
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
            GetCharacterComponent<CharacterFSM>().ChangeState(typeof(RecoveryState));
            GetCharacterComponent<PlayerVisual>().Ragdoll.SetActive(false, true);
            GetCharacterComponent<PlayerMovement>().Teleport(pos, Quaternion.Euler(rot));
        }

        public void Respawn()
        {   
            GetCharacterComponent<PlayerVisual>().Ragdoll.SetActive(false, true);
            GetCharacterComponent<PlayerMovement>().Teleport(respawnPoint.position, respawnPoint.rotation);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            if(IsOwner)
            {
                MinigameSpectate spectate = MinigameManager.Instance.CurrentMinigame.GetComponent<MinigameSpectate>();
                if (spectate == null)
                    return;

                spectate.StartSpectate();
            }
        }
    }
}
