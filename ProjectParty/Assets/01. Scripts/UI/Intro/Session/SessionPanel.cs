using System.Collections;
using OMG.Network;
using Steamworks.Data;
using UnityEngine;

namespace OMG.UI.Sessions
{
    public class SessionPanel : MonoBehaviour
    {
        [SerializeField] SessionSlot slotPrefab = null;
        [SerializeField] Transform container = null;

        [SerializeField] float updateDelay = 1f;

        private void Start()
        {
            Display(false);
        }

        private void OnEnable()
        {
            StartCoroutine(UpdateRoutine());
        }

        private void OnDisable()
        {
            StopAllCoroutines();    
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

            Lobby[] lobbies = await ClientManager.Instance.GetLobbyListAsync(owner);
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

        private IEnumerator UpdateRoutine()
        {
            YieldInstruction delay = new WaitForSeconds(updateDelay);
            
            while(gameObject.activeInHierarchy)
            {
                FillSlots(null);
                yield return delay;
            }
        }
    }
}
