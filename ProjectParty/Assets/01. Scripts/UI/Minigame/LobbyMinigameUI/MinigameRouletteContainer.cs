using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI
{
    public class MinigameRouletteContainer : MonoBehaviour
    {
        [SerializeField] private MinigameListSO minigameListSO;

        [Space]
        [SerializeField] private Transform slotContainerTrm;
        [SerializeField] private MinigameSlot slotPrefab;
        [SerializeField] private float padding;
        [SerializeField] private float moveSpeed;

        private void Update()
        {
            if(gameObject.activeSelf)
            {
                foreach(Transform slotTrm in slotContainerTrm)
                {
                    slotTrm.localPosition += -Vector3.right * moveSpeed;
                }
            }
        }

        public void Show()
        {
            foreach(Transform slot in slotContainerTrm)
            {
                Destroy(slot.gameObject);
            }

            for(int i = 0; i < minigameListSO.Count; i++)
            {
                Vector3 spawnPos = slotContainerTrm.position + slotContainerTrm.right * padding * i;
                MinigameSlot slot = Instantiate(slotPrefab, slotContainerTrm);
                slot.transform.position = spawnPos;
                //slot.transform.localRotation = transform.rotation;
            }

            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}