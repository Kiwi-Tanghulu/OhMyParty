using OMG.FSM;
using OMG.NetworkEvents;
using UnityEngine;

namespace OMG.Player
{
    public class PlayerController : CharacterController, IRespawn
    {
        public NetworkEvent<TransformParams> RespawnFunction = new NetworkEvent<TransformParams>("RespawnFunction");

        protected override bool Init()
        {
            bool baseInit = base.Init();
            if(baseInit == false)
                return false;

            RespawnFunction.AddListener(Respawn);
            RespawnFunction.Register(NetworkObject);
            
            return true;
        }

        private void Respawn(TransformParams spawnPosition)
        {
            if(IsOwner == false)
                return;

            Respawn(spawnPosition.Position, spawnPosition.Rotation);
        }

        public virtual void Respawn(Vector3 pos, Vector3 rot)
        {
            //if(IsOwner == false)
            //{
            //    Debug.LogError($"Respawn should called by owner");
            //    return;
            //}

            GetCharacterComponent<CharacterFSM>().ChangeDefaultState();
            GetCharacterComponent<PlayerVisual>().Ragdoll.SetActive(false);
            GetCharacterComponent<PlayerMovement>().Teleport(pos, Quaternion.Euler(rot));
        }
    }
}