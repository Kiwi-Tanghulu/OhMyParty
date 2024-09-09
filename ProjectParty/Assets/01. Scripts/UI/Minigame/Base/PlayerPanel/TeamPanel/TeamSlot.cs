using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI.Minigames
{
    public class TeamSlot : MonoBehaviour
    {
        [SerializeField] PlayerSlot playerSlotPrefab = null;
        [SerializeField] Transform container = null;
        protected List<PlayerSlot> slots = null;

        protected virtual void Awake()
        {
            slots = new List<PlayerSlot>();
        }

        public PlayerSlot AddTeamMember(ulong clientID)
        {
            PlayerSlot slot = Instantiate(playerSlotPrefab, container);
            slot.SetClientID(clientID);
            slots.Add(slot);

            return slot;
        }
    }
}
