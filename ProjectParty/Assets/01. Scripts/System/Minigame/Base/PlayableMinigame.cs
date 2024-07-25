using System.Collections.Generic;
using OMG.Attributes;
using OMG.Extensions;
using OMG.Player;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames
{
    public abstract class PlayableMinigame : Minigame
    {
        [SerializeField] protected PlayerController playerPrefab = null;
        [SerializeField] bool useLife = true;
        [ConditionalField("useLife", true)]
        [SerializeField] int lifeCount = 3;

        [SerializeField] List<Transform> spawnPositions = null;
        protected virtual bool ShufflePosition => false;

        private Dictionary<ulong, PlayerController> playerDictionary = null;
        public Dictionary<ulong, PlayerController> PlayerDictionary => playerDictionary;

        private NetworkList<NetworkObjectReference> players = null;
        public NetworkList<NetworkObjectReference> Players => players;

        protected override void Awake()
        {
            base.Awake();
            players = new NetworkList<NetworkObjectReference>();
        }

        public override void SetPlayerDatas(params ulong[] playerIDs)
        {
            base.SetPlayerDatas(playerIDs);
            playerDictionary = new Dictionary<ulong, PlayerController>();

            if(useLife)
            {
                playerDatas.ChangeAllData(i => {
                    i.lifeCount = lifeCount;
                    return i;
                });
            }
        }

        public override void StartGame()
        {
            base.StartGame();
            
            if(IsHost == false)
                return;
            Debug.Log("Player Create");
            
            if(ShufflePosition)
                spawnPositions = spawnPositions.Shuffle();
            for (int i = 0; i < playerDatas.Count; ++i)
                SpawnPlayer(i);
        }

        public override void Release()
        {
            base.Release();

            if(IsHost == false)
                return;

            players.ForEach(i => {
                if(i.TryGet(out NetworkObject player))
                    player.Despawn();
            });
        }

        // it should called by host
        public void RespawnPlayer(ulong clientID)
        {
            Transform position = spawnPositions.PickRandom();
            playerDictionary[clientID].RespawnFunction.Broadcast(position, false);
        }

        protected PlayerController SpawnPlayer(int index)
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
            // NetworkTransform trm = player.GetComponent<NetworkTransform>();
            // trm.Teleport(position, Quaternion.identity, trm.GetScale());
            //player.GetComponent<CharacterMovement>().Teleport(position, Quaternion.identity);
            players.Add(player);
        }
    }
}
