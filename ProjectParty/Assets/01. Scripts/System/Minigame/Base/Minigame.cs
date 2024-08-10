using OMG.Extensions;
using OMG.Inputs;
using OMG.NetworkEvents;
using OMG.UI;
using OMG.UI.Minigames;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using NetworkEvent = OMG.NetworkEvents.NetworkEvent;

namespace OMG.Minigames
{
    public abstract class Minigame : NetworkBehaviour
    {
        [SerializeField] MinigameSO minigameData = null;
        public MinigameSO MinigameData => minigameData;

        public NetworkEvent OnStartEvent = new NetworkEvent("MinigameStart");
        public NetworkEvent OnFinishEvent = new NetworkEvent("MinigameFinish");

        protected NetworkList<PlayerData> playerDatas = null;
        public NetworkList<PlayerData> PlayerDatas => playerDatas;

        protected MinigamePanel minigamePanel = null;
        public MinigamePanel MinigamePanel => minigamePanel;

        protected CutscenePanel cutscenePanel = null;
        public CutscenePanel CutscenePanel => cutscenePanel;

        protected MinigameCycle cycle = null;
        public MinigameCycle Cycle => cycle;

        public bool IsPlaying { get; private set; } = false;

        protected virtual void Awake()
        {
            cutscenePanel = DEFINE.MinigameCanvas?.transform.Find("CutscenePanel").GetComponent<CutscenePanel>();
            minigamePanel = DEFINE.MinigameCanvas?.transform.Find("MinigamePanel").GetComponent<MinigamePanel>();

            playerDatas = new NetworkList<PlayerData>();
            cycle = GetComponent<MinigameCycle>();

            cycle.Init(this);
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            MinigameManager.Instance.CurrentMinigame = this;

            OnStartEvent.AddListener(OnGameStart);
            OnStartEvent.Register(NetworkObject);

            OnFinishEvent.AddListener(OnGameFinish);
            OnFinishEvent.Register(NetworkObject);
        }

        /// <summary>
        /// Only Host Could Call this Method
        /// </summary>  
        public virtual void SetPlayerDatas(params ulong[] playerIDs)
        {
            for (int i = 0; i < playerIDs.Length; ++i)
                playerDatas.Add(new PlayerData(playerIDs[i]));
        }

        public virtual void Init()
        {
            cutscenePanel.Init(this);

            Fade.Instance.FadeIn(
                3f,
                () => MinigameManager.Instance.MinigamePaused = true,
                () =>
                {
                    MinigameManager.Instance.MinigamePaused = false;
                    InputManager.SetInputEnable(true);
                }
            );

            StartCoroutine(this.DelayCoroutine(minigameData.IntroPostponeTime, () => {
                cycle.PlayIntro();
            }));
        }

        public virtual void Release() 
        {
        }

        public void StartGame()
        {   
            OnStartEvent?.Broadcast();
        }

        protected virtual void OnGameStart(NoneParams ignore)
        {
            InputManager.ChangeInputMap(InputMapType.Play);
            GameManager.Instance.CursorActive = false;
            minigamePanel.Init(this);
            minigamePanel.Display(true);

            IsPlaying = true;
        }

        public void FinishGame() 
        {
            OnFinishEvent?.Broadcast();
        }

        protected virtual void OnGameFinish(NoneParams ignore)
        {
            InputManager.ChangeInputMap(InputMapType.UI);
            GameManager.Instance.CursorActive = true;
            IsPlaying = false;
            
            StartCoroutine(this.DelayCoroutine(minigameData.OutroPostponeTime, () => {
                cycle.PlayOutro();
            }));
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            Fade.Instance.FadeIn(
                3f, 
                null,//() => MinigameManager.Instance.MinigamePaused = true, 
                () =>
                {
                    //MinigameManager.Instance.MinigamePaused = false;
                    InputManager.SetInputEnable(true);
                }
            );
        }

        public virtual int CalculateScore(int origin) => origin;

        public void DespawnMinigameObject(NetworkObject target, bool ignoreOwnership = false)
        {
            if (ignoreOwnership)
                DespawnObjectServerRpc(target);
            else
                target.Despawn();
        }

        [ServerRpc(RequireOwnership = false)]
        private void DespawnObjectServerRpc(NetworkObjectReference targetReference)
        {
            if (targetReference.TryGet(out NetworkObject target))
                target.Despawn();
        }
    }
}
