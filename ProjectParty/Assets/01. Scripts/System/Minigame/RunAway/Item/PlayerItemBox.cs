using JetBrains.Annotations;
using OMG.Items;
using OMG.Lobbies;
using OMG.Player;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering;

namespace OMG.Minigames.RunAway
{
    public class PlayerItemBox : NetworkBehaviour
    {
        [SerializeField] private bool isRandomItem;
        [SerializeField] private PlayerItem item;
        [SerializeField] private List<PlayerItem> itemList;

        [Space]
        public UnityEvent OnGetItemEvent;

        private void Start()
        {
            if (isRandomItem)
            {
                int randomIndex = UnityEngine.Random.Range(0, itemList.Count);
                item = itemList[randomIndex];
            }

            if(item != null)
            {
                if(itemList.Contains(item) == false)
                {
                    Debug.LogError("not exist item in itemList");
                    return;
                }
            }

            NetworkObject.Spawn(true);
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            Debug.Log(123);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsHost == false)
                return;
            if (item == null)
                return;

            if(other.TryGetComponent<PlayerController>(out PlayerController player))
            {
                GetItemClientRpc(player.OwnerClientId, itemList.IndexOf(item));
            }
        }

        [ClientRpc]
        private void GetItemClientRpc(ulong playerID, int itemIndex)
        {
            if (NetworkManager.LocalClientId != playerID)
                return;

            PlayableMinigame minigame = MinigameManager.Instance.CurrentMinigame as PlayableMinigame;
            if (minigame == null)
                return;

            PlayerController player = minigame.PlayerDictionary[playerID];
            if (player == null)
                return;

            if(player.TryGetComponent<PlayerItemHolder>(out PlayerItemHolder holder))
            {
                PlayerItem item = Instantiate(itemList[itemIndex]);
                item.NetworkObject.SpawnWithOwnership(playerID);
                holder.GetItem(item);
                OnGetItemEvent?.Invoke();
                NetworkObject.Despawn(true);
            }
        }
    }
}