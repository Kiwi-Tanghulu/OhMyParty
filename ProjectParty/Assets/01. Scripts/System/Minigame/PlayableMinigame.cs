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

        private NetworkList<NetworkObjectReference> players = null;
        public NetworkList<NetworkObjectReference> Players => players;

        protected override void Awake()
        {
            base.Awake();
            players = new NetworkList<NetworkObjectReference>();
        }

        public override void Release()
        {
            base.Release();
            players.ForEach(i => {
                if(i.TryGet(out NetworkObject player))
                    player.Despawn();
            });
        }

        protected void CreatePlayer(int index)
        {
            PlayerController player = Instantiate(playerPrefab, spawnPositions[index].position, Quaternion.identity);
            RegisterPlayer(player.NetworkObject, playerDatas[index].clientID);
        }

        public void RegisterPlayer(NetworkObject player, ulong clientID)
        {
            player.SpawnWithOwnership(clientID, true);
            player.TrySetParent(NetworkObject);
            players.Add(player);
        }
    }
}
