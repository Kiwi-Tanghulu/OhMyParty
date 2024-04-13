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
            Debug.Log("Client Player Setting");
            PlayerController player = Instantiate(playerPrefab, spawnPositions[index].position, Quaternion.identity);
            player.NetworkObject.SpawnWithOwnership(PlayerDatas[index].clientID, true);
            player.NetworkObject.TrySetParent(NetworkObject);

            RegisterPlayer(player.NetworkObject);
        }

        public void RegisterPlayer(NetworkObject player)
        {
            players.Add(player);
            Debug.Log($"Player Count : {players.Count}");
            // players.Sort((a, b) => (int)a.OwnerClientId - (int)b.OwnerClientId);
        }
    }
}
