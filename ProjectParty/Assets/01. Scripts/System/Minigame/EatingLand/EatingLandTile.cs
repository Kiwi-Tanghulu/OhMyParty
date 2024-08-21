using OMG.Extensions;
using OMG.Lobbies;
using System;
using UnityEngine;


namespace OMG.Minigames.EatingLand
{
    public class EatingLandTile : MonoBehaviour
    {
        private int tileID;
        private ulong currentOwnerId = ulong.MaxValue;

        private EatingLandTileVisual eatingLandTileVisual;
        public event Action<int, ulong, ulong> OnUpdateTileEvent;

        private void Awake()
        {
            eatingLandTileVisual = GetComponent<EatingLandTileVisual>();
        }
        public void Init(int id)
        {
            tileID = id;
            currentOwnerId = ulong.MaxValue;
        }

        public void OnPlayerEnter(ulong nextOwnerId)
        {
            if (currentOwnerId == nextOwnerId)
            {
                return;
            }
            OnUpdateTileEvent?.Invoke(tileID, currentOwnerId , nextOwnerId);
        }

        public void UpdateTile(ulong nextOwnerId)
        {
            int nextIndex = Lobby.Current.PlayerDatas.Find(out Lobbies.PlayerData data, data => data.ClientID == nextOwnerId);
            eatingLandTileVisual.ChangeVisual(nextIndex);
            currentOwnerId = nextOwnerId;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerEnter(other.GetComponent<EatingLandPlayerController>().OwnerClientId);
            }
        }
    }
}
