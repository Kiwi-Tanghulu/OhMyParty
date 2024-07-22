using OMG.FSM;
using OMG.Lobbies;
using Steamworks;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerController : CharacterController, IRespawn
    {
        public void Respawn(Vector3 pos, Vector3 rot)
        {
            GetCharacterComponent<CharacterFSM>().ChangeDefaultState();
            GetCharacterComponent<PlayerVisual>().Ragdoll.SetActive(false);
            GetCharacterComponent<PlayerMovement>().Teleport(pos, Quaternion.Euler(rot));
        }
    }
}