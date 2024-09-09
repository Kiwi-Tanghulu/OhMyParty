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

        private CharacterFSM fsm;
        private PlayerVisual visual;
        private PlayerMovement movement;

        protected override bool Init()
        {
            bool result = base.Init();

            fsm = GetCharacterComponent<CharacterFSM>();
            visual = GetCharacterComponent<PlayerVisual>();
            movement = GetCharacterComponent<PlayerMovement>();

            return result;
        }

        public void SetRespawnPoint(Transform point)
        {
            respawnPoint = point;
        }

        public override void Respawn(Vector3 pos, Vector3 rot)
        {
            if (fsm.CurrentState.GetType() != typeof(StunState))
                fsm.ChangeState(typeof(RecoveryState));
            visual.Ragdoll.SetActive(false, true);
            movement.Teleport(pos, Quaternion.Euler(rot));
        }

        public void Respawn()
        {
            if (fsm.CurrentState.GetType() != typeof(StunState))
                fsm.ChangeState(typeof(RecoveryState));
            visual.Ragdoll.SetActive(false, true);
            movement.Teleport(respawnPoint.position, respawnPoint.rotation);
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
