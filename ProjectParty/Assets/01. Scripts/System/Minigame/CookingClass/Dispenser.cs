using System.Collections;
using System.Collections.Generic;
using OMG.Extensions;
using OMG.NetworkEvents;
using OMG.UI.Minigames;
using UnityEngine;

namespace OMG.Minigames.CookingClass
{
    public class Dispenser : CharacterController
    {
        [SerializeField] float dispenseDelay = 2f;

        [Space(15f)]
        private NetworkEvent<IntParams, int> OnDispenseEvent = new NetworkEvent<IntParams, int>("dispense"); 

        private Minigame minigame = null;
        private Dictionary<ulong, Coroutine> dispenseTable = null;

        private PlayerPanel playerPanel = null;

        public void Init(Minigame minigame)
        {
            dispenseTable = new Dictionary<ulong, Coroutine>();
            minigame.PlayerDatas.ForEach(i => dispenseTable.Add(i.clientID, null));

            this.minigame = minigame;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            OnDispenseEvent.AddListener(HandleDispense);
            OnDispenseEvent.Register(NetworkObject);

            playerPanel = MinigameManager.Instance.CurrentMinigame.MinigamePanel.PlayerPanel;
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            OnDispenseEvent.RemoveListener(HandleDispense);
            OnDispenseEvent.Unregister();
        }

        public void Release()
        {
            isInit = false;
            minigame.PlayerDatas.ForEach(i => {
                RemoveDispenseTable(i.clientID);
            });
            dispenseTable.Clear();
        }

        public void AddDispenseTable(ulong clientID)
        {
            if(dispenseTable[clientID] != null)
                StopCoroutine(dispenseTable[clientID]);

            dispenseTable[clientID] = StartCoroutine(DispenseCoroutine(dispenseDelay, clientID));
        }

        public void RemoveDispenseTable(ulong clientID)
        {
            if(dispenseTable[clientID] == null)
                return;

            StopCoroutine(dispenseTable[clientID]);
            dispenseTable[clientID] = null;
        }

        private IEnumerator DispenseCoroutine(float delay, ulong clientID)
        {
            yield return new WaitForSeconds(delay);

            int playerIndex = minigame.PlayerDatas.Find(out PlayerData playerData, i => i.clientID == clientID);

            playerData.score += 1;
            minigame.PlayerDatas[playerIndex] = playerData;

            OnDispenseEvent?.Broadcast(playerIndex);

            AddDispenseTable(clientID);
        }

        private void HandleDispense(int index)
        {
            ScorePlayerSlot slot = playerPanel[index] as ScorePlayerSlot;
            slot.SetScore(slot.Score + 1);
        }
    }
}
