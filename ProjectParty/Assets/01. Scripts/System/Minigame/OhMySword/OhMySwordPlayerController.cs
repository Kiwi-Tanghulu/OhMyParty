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
        [SerializeField] float respawnDelay = 1f;
        private CatchTailPlayer catchTailPlayer = null;

        private ScorePlayerPanel playerPanel = null;
        private OhMySword minigame = null;
        private OhMySwordCycle cycle = null;
        private int playerIndex = 0;

        private Coroutine xpUpdateRoutine = null;
        private int xpBuffer = 0;
        private int prevXP = 0;

        private NetworkEvent<IntParams, int> onUpdateXPEvent = new NetworkEvent<IntParams, int>("UpdateXP");

        protected override void Awake()
        {
            base.Awake();
            minigame = MinigameManager.Instance?.CurrentMinigame as OhMySword;
            cycle = minigame.Cycle as OhMySwordCycle;

            playerPanel = minigame.MinigamePanel.PlayerPanel as ScorePlayerPanel;
            playerIndex = minigame.PlayerDatas.Find(out PlayerData data, data => data.clientID == OwnerClientId);

            catchTailPlayer = GetComponent<CatchTailPlayer>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            onUpdateXPEvent.AddListener(HandleXP);
            onUpdateXPEvent.Register(NetworkObject);

            SetActiveUpdateRoutine(true);
            sword.Init(NetworkObject);
            catchTailPlayer.Init(NetworkObject);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            SetActiveUpdateRoutine(false);
            onUpdateXPEvent?.Unregister();
        }

        private void SetActiveUpdateRoutine(bool active)
        {
            if(active)
                xpUpdateRoutine = StartCoroutine(XPUpdateRoutine());
            else
                StopCoroutine(xpUpdateRoutine);
        }

        public void Respawn()
        {
            if(IsHost == false)
                return;
            
            StartCoroutine(this.DelayCoroutine(respawnDelay, () => {
                cycle.Respawn(OwnerClientId);
            }));
        }

        public void GetXP(int amount)
        {
            xpBuffer += amount;
            sword.SetLength(xpBuffer);
            playerPanel.SetScore(playerIndex, xpBuffer);
        }

        private void HandleXP(int xp)
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
