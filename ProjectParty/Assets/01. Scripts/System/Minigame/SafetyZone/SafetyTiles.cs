using OMG.Extensions;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTiles : NetworkBehaviour
    {
        [SerializeField] SafetyTile[] tiles = null;
        [SerializeField] float fallingPostpone = 2f;
        
        [Space(15f)]
        [SerializeField] UnityEvent onRerollEvent = null;
        [SerializeField] UnityEvent onDecisionEvent = null;
        [SerializeField] UnityEvent onResetEvent = null;

        private GameObject groundCollider = null;

        private void Awake()
        {
            groundCollider = transform.Find("GroundCollider").gameObject;
        }

        public void RerollTiles()
        {
            Debug.Log("Reroll");
            for(int i = 0; i < tiles.Length; ++i)
            {
                int safetyNumber = Random.Range(0, 4);
                UpdateSafetyNumberClientRpc(i, safetyNumber);
            }

            RerollTilesClientRpc();
        }

        public void DecisionSafetyZone()
        {
            Debug.Log("Decision");
            tiles.ForEach((i, index) => {
                if(i.IsSafetyZone())
                    return;
                
                TileActiveClientRpc(index);
            });

            DecisionSafetyZoneClientRpc();
        }

        public void ResetTiles()
        {
            Debug.Log("Reset");
            ResetTilesClientRpc();
        }

        public void Init()
        {
            tiles.ForEach(i => i.Init());
        }

        [ClientRpc]
        private void RerollTilesClientRpc()
        {
            onRerollEvent?.Invoke();
        }

        [ClientRpc]
        private void DecisionSafetyZoneClientRpc()
        {
            StartCoroutine(this.DelayCoroutine(fallingPostpone, () => groundCollider.SetActive(false)));
            onDecisionEvent?.Invoke();
        }

        [ClientRpc]
        private void UpdateSafetyNumberClientRpc(int index, int number)
        {
            tiles[index].SetSafetyNumber(number);
        }

        [ClientRpc]
        private void TileActiveClientRpc(int index)
        {
            tiles[index].SetActive(false);
        }

        [ClientRpc]
        private void ResetTilesClientRpc()
        {
            tiles.ForEach(i => {
                i.Reset();
            });
            groundCollider.SetActive(true);
            onResetEvent?.Invoke();
        }
    }
}
