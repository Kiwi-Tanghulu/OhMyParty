using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventurePlayerParticle : NetworkBehaviour
    {
        [SerializeField] private ParticleSystem getItemParticle;


        [ServerRpc]
        public void GetItemParticleServerRPC()
        {
            GetItemParticleClientRPC();
        }

        [ClientRpc]
        public void GetItemParticleClientRPC()
        {
            Instantiate(getItemParticle, transform.position, Quaternion.identity).Play();
        }
    }
}
