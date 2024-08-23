using OMG.Networks;
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

            INetworkLobby[] lobbies = await GuestManager.Instance.GetLobbyListAsync();
            if(lobbies == null)
                return;

            foreach(INetworkLobby lobby in lobbies)
                CreateSlot(lobby);
        }

        public void ClearSlots()
        {
            foreach(Transform trm in container)
                Destroy(trm.gameObject);
        }

        private void CreateSlot(INetworkLobby lobby)
        {
            SessionSlot slot = Instantiate(slotPrefab, container);
            slot.Init(lobby);
        }
    }
}
