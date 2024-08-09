using System.Collections;
using OMG.ETC;
using OMG.Extensions;
using OMG.NetworkEvents;
using OMG.Player;
using OMG.UI.Minigames.OhMySword;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class OhMySwordPlayerController : PlayerController
    {
        [SerializeField] PlayerColorController tail = null;
        [SerializeField] Sword sword = null;
        [SerializeField] float respawnDelay = 1f;
        private CatchTailPlayer catchTailPlayer = null;
        private ScoreContainer scoreContainer = null;

        private OhMySwordPlayerPanel playerPanel = null;
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

            playerPanel = minigame.MinigamePanel.PlayerPanel as OhMySwordPlayerPanel;

            catchTailPlayer = GetComponent<CatchTailPlayer>();
            scoreContainer = GetComponent<ScoreContainer>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            
            playerIndex = minigame.PlayerDatas.Find(out PlayerData data, data => data.clientID == OwnerClientId);
            if(IsOwner)
                Destroy(tail.gameObject);
            else
            {
                tail.SetIndex(playerIndex);
                tail.SetColor();
            }

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

        public void HandleDead()
        {
            sword.ResetLength();
            if(IsOwner)
            {
                StartCoroutine(this.PostponeFrameCoroutine(() => {
                    GetXP(-xpBuffer);
                    UpdateXP();
                }));
            }
            
            if(IsHost == false)
                return;
            
            minigame.PlayerDatas.Find(out PlayerData player, i => i.clientID == OwnerClientId);

            scoreContainer.Init(player.score, minigame.ScoreContainer);
            scoreContainer.GenerateXP();
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

            if(minigame.IsPlaying == false)
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
                if(IsSpawned == false)
                    yield return delay;

                if(xpBuffer != prevXP)
                    UpdateXP();

                yield return delay;
            }
        }

        private void UpdateXP()
        {
            onUpdateXPEvent?.Broadcast(xpBuffer);
            prevXP = xpBuffer;
        }
    }
}
