using OMG.Network;
using Steamworks.Data;
using UnityEngine;

namespace OMG.UI.Sessions
{
    public class SessionPanel : MonoBehaviour
    {
        [SerializeField] SessionSlot slotPrefab = null;
        [SerializeField] Transform container = null;

        private void Start()
        {
            Display(false);
        }

        public void Display(bool active)
        {
            if(active)
                FillSlots(null);

            gameObject.SetActive(active);
        }

        public async void FillSlots(string owner)
        {
            ClearSlots();

            Lobby[] lobbies = await ClientManager_.Instance.GetLobbyListAsync(owner);
            if(lobbies == null)
                return;

            foreach(Lobby lobby in lobbies)
                CreateSlot(lobby);
        }

        public void ClearSlots()
        {
            foreach(Transform trm in container)
                Destroy(trm.gameObject);
        }

        private void CreateSlot(Lobby lobby)
        {
            SessionSlot slot = Instantiate(slotPrefab, container);
            slot.Init(lobby);
        }
    }
}
