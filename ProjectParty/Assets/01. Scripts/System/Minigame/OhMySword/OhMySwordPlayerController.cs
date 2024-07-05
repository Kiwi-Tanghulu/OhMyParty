using System.Collections;
using OMG.Extensions;
using OMG.NetworkEvents;
using OMG.Player;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class OhMySwordPlayerController : PlayerController
    {
        [SerializeField] Collider playerCollider = null;
        [SerializeField] Sword sword = null;

        private Minigame minigame = null;

        private Coroutine xpUpdateRoutine = null;
        private int xpBuffer = 0;
        private int prevXP = 0;

        private NetworkEvent<IntParams> onUpdateXPEvent = new NetworkEvent<IntParams>("UpdateXP");

        protected override void Awake()
        {
            base.Awake();
            minigame = MinigameManager.Instance.CurrentMinigame;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            onUpdateXPEvent.AddListener(HandleXP);
            onUpdateXPEvent.Register(NetworkObject);
            playerCollider.enabled = IsOwner;

            SetActive(true);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            SetActive(false);
            onUpdateXPEvent?.Unregister();
        }

        private void SetActive(bool active)
        {
            if(active)
                xpUpdateRoutine = StartCoroutine(XPUpdateRoutine());
            else
                StopCoroutine(xpUpdateRoutine);
        }

        public void GetXP(int amount)
        {
            xpBuffer += amount;
            sword.SetLength(amount);
        }

        private void HandleXP(IntParams xp)
        {
            // Display UI
            if(IsOwner == false)
                sword.SetLength(xp);

            if(IsHost == false)
                return;

            minigame.PlayerDatas.ChangeData(i => i.clientID == OwnerClientId, data => {
                data.score = xp;
                return data;
            });
        }

        private IEnumerator XPUpdateRoutine()
        {
            YieldInstruction delay = new WaitForSeconds(1f);

            while(true)
            {
                if(xpBuffer != prevXP)
                {
                    onUpdateXPEvent?.Broadcast(xpBuffer);
                    prevXP = xpBuffer;
                }

                yield return delay;
            }
        }
    }
}
