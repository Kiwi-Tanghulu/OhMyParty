using System.Collections.Generic;
using OMG.Extensions;
using OMG.Players;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames
{
    public abstract class PlayableMinigame : Minigame
    {
        [SerializeField] protected PlayerController playerPrefab = null;
        [SerializeField] Transform[] spawnPositions = null;

        private Dictionary<ulong, PlayerController> playerDictionary = null;
        public Dictionary<ulong, PlayerController> PlayerDictionary => playerDictionary;

        private NetworkList<NetworkObjectReference> players = null;
        public NetworkList<NetworkObjectReference> Players => players;

        protected override void Awake()
        {
            base.Awake();
            players = new NetworkList<NetworkObjectReference>();
        }

        public override void Init(params ulong[] playerIDs)
        {
            base.Init(playerIDs);
            playerDictionary = new Dictionary<ulong, PlayerController>();   
        }

        public override void Release()
        {
            base.Release();
            players.ForEach(i => {
                if(i.TryGet(out NetworkObject player))
                    player.Despawn();
            });
        }

        protected PlayerController CreatePlayer(int index)
        {
            PlayerController player = Instantiate(playerPrefab, spawnPositions[index].position, Quaternion.identity);
            RegisterPlayer(player.NetworkObject, playerDatas[index].clientID);
            playerDictionary.Add(playerDatas[index].clientID, player);

            return player;
        }

        public void RegisterPlayer(NetworkObject player, ulong clientID)
        {
            player.SpawnWithOwnership(clientID, true);
            player.TrySetParent(NetworkObject);
            players.Add(player);
        }
    }
}