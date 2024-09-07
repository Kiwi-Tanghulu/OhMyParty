using System.Collections;
using System.Collections.Generic;
using OMG.Extensions;
using OMG.NetworkEvents;
using UnityEngine;

namespace OMG.Minigames.CookingClass
{
    public class Dispenser : CharacterController
    {
        [SerializeField] float dispenseDelay = 2f;

        [Space(15f)]
        public NetworkEvent<UlongParams, ulong> OnDispenseEvent = new NetworkEvent<UlongParams, ulong>("dispense"); 

        private Minigame minigame = null;
        private Dictionary<ulong, Coroutine> dispenseTable = null;

        public void Init(Minigame minigame)
        {
            dispenseTable = new Dictionary<ulong, Coroutine>();
            minigame.PlayerDatas.ForEach(i => dispenseTable.Add(i.clientID, null));

            OnDispenseEvent.Register(NetworkObject);

            this.minigame = minigame;
        }
        
        public void Release()
        {
            OnDispenseEvent.Unregister();
        }

        public void AddDispenseTable(ulong clientID)
        {
            if(dispenseTable[clientID] != null)
                StopCoroutine(dispenseTable[clientID]);

            dispenseTable[clientID] = StartCoroutine(DispenseCoroutine(dispenseDelay, clientID));
        }

        public void RemoveDispenseTable(ulong clientID)
        {
            StopCoroutine(dispenseTable[clientID]);
            dispenseTable[clientID] = null;
        }

        private IEnumerator DispenseCoroutine(float delay, ulong clientID)
        {
            yield return new WaitForSeconds(delay);

            OnDispenseEvent?.Broadcast(clientID);
            minigame.PlayerDatas.ChangeData(i => i.clientID == clientID, i => {
                i.score += 1;
                return i;
            });

            AddDispenseTable(clientID);
        }
    }
}
