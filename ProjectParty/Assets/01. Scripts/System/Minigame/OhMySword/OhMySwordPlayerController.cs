using System.Collections;
using OMG.Extensions;
using OMG.NetworkEvents;
using OMG.Player;
using OMG.UI.Minigames;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class OhMySwordPlayerController : PlayerController
    {
        // [SerializeField] Collider playerCollider = null;
        [SerializeField] Sword sword = null;

        private ScorePlayerPanel playerPanel = null;
        private OhMySword minigame = null;
        private int playerIndex = 0;

        private Coroutine xpUpdateRoutine = null;
        private int xpBuffer = 0;
        private int prevXP = 0;

        private NetworkEvent<IntParams> onUpdateXPEvent = new NetworkEvent<IntParams>("UpdateXP");

        protected override void Awake()
        {
            base.Awake();
            minigame = MinigameManager.Instance?.CurrentMinigame as OhMySword;
            playerPanel = minigame.MinigamePanel.PlayerPanel as ScorePlayerPanel;
            playerIndex = minigame.PlayerDatas.Find(out PlayerData data, data => data.clientID == OwnerClientId);
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            onUpdateXPEvent.AddListener(HandleXP);
            onUpdateXPEvent.Register(NetworkObject);

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

        public void Respawn()
        {
            if(IsHost)
                minigame.RespawnPlayer(OwnerClientId);
        }

        public void GetXP(int amount)
        {
            xpBuffer += amount;
            sword.SetLength(xpBuffer);
            playerPanel.SetScore(playerIndex, xpBuffer);
        }

        private void HandleXP(IntParams xp)
        {
            if(IsOwner == false)
            {
                sword.SetLength(xp);
                playerPanel.SetScore(playerIndex, xp);
            }

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
