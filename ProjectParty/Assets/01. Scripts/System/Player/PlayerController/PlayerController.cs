using OMG.FSM;
using OMG.Lobbies;
using Steamworks;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerController : CharacterController
    {
        public override void Respawn(Transform respawnTrm)
        {
            base.Respawn(respawnTrm);

            GetCharacterComponent<CharacterFSM>().ChangeDefaultState();
            GetCharacterComponent<PlayerVisual>().Ragdoll.SetActive(false);
            GetCharacterComponent<PlayerMovement>().Teleport(respawnTrm.position, respawnTrm.rotation);
        }
    }
}