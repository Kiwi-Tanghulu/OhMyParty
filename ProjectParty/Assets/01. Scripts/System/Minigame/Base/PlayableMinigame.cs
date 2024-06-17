using System.Collections.Generic;
using OMG.Extensions;
using OMG.Player;
using Unity.Netcode;
using Unity.Netcode.Components;
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

        public override void StartGame()
        {
            base.StartGame();
            
            if(IsHost == false)
                return;
            Debug.Log("Player Create");
            for (int i = 0; i < playerDatas.Count; ++i)
                CreatePlayer(i);
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
            RegisterPlayer(player.NetworkObject, spawnPositions[index].position, playerDatas[index].clientID);
            playerDictionary.Add(playerDatas[index].clientID, player);

            return player;
        }

        public void RegisterPlayer(NetworkObject player, Vector3 position, ulong clientID)
        {
            player.SpawnWithOwnership(clientID, true);
            player.TrySetParent(NetworkObject);
            //player.GetComponent<CharacterMovement>().Teleport(position, Quaternion.identity);
            players.Add(player);
        }
    }
}
