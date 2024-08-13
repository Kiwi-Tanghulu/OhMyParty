using OMG.Extensions;
using OMG.Inputs;
using OMG.UI;
using OMG.UI.Minigames;
using Unity.Netcode;
using UnityEngine;
using NetworkEvent = OMG.NetworkEvents.NetworkEvent;
using StateType = OMG.Minigames.MinigameState.StateType;

namespace OMG.Minigames
{
    public abstract class Minigame : NetworkBehaviour
    {
        [SerializeField] MinigameSO minigameData = null;
        public MinigameSO MinigameData => minigameData;

        public NetworkEvent OnInitEvent = new NetworkEvent("Init");
        public NetworkEvent OnReleaseEvent = new NetworkEvent("Release");
        public NetworkEvent OnStartEvent = new NetworkEvent("Start");
        public NetworkEvent OnFinishEvent = new NetworkEvent("Finish");

        protected NetworkList<PlayerData> playerDatas = null;
        public NetworkList<PlayerData> PlayerDatas => playerDatas;

        protected MinigamePanel minigamePanel = null;
        public MinigamePanel MinigamePanel => minigamePanel;

        protected CutscenePanel cutscenePanel = null;
        public CutscenePanel CutscenePanel => cutscenePanel;

        protected MinigameCycle cycle = null;
        public MinigameCycle Cycle => cycle;

        protected MinigameState state = null;
        public MinigameState State => state;

        private MinigameCutscene cutscene = null;

        protected virtual void Awake()
        {
            cutscenePanel = DEFINE.MinigameCanvas?.transform.Find("CutscenePanel").GetComponent<CutscenePanel>();
            minigamePanel = DEFINE.MinigameCanvas?.transform.Find("MinigamePanel").GetComponent<MinigamePanel>();

            playerDatas = new NetworkList<PlayerData>();
            cycle = GetComponent<MinigameCycle>();
            cutscene = GetComponent<MinigameCutscene>();
            state = GetComponent<MinigameState>();

            cycle.Init(this);
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            MinigameManager.Instance.CurrentMinigame = this;

            OnInitEvent.AddListener(HandleGameInit);
            OnInitEvent.Register(NetworkObject);

            OnReleaseEvent.AddListener(HandleGameRelease);
            OnReleaseEvent.Register(NetworkObject);

            OnStartEvent.AddListener(HandleGameStart);
            OnStartEvent.Register(NetworkObject);

            OnFinishEvent.AddListener(HandleGameFinish);
            OnFinishEvent.Register(NetworkObject);

            state.Init();
            state.OnStateSyncedEvent += HandleStateSynced;
            if(IsHost == false) // 호스트가 아니면 OnNetworkSpawned 에서 상태 변경
                state.ChangeMinigameState(StateType.Spawned);
        }

        public virtual void SetPlayerDatas(ulong[] playerIDs)
        {
            if (IsHost == false)
            {
                Debug.LogError("Lifecycle methods should only be called by the host.");
                return;
            }

            for (int i = 0; i < playerIDs.Length; ++i)
                playerDatas.Add(new PlayerData(playerIDs[i]));

            state.Init(playerIDs);
            state.ChangeMinigameState(StateType.Spawned); // 호스트는 SetPlayerDatas에서 상태 변경
        }

        #region Init
        public void Init()
        {
            if(IsHost == false)
            {
                Debug.LogError("Lifecycle methods should only be called by the host.");
                return;
            }
                
            OnInitEvent?.Broadcast();
        }

        private void HandleGameInit()
        {
            OnGameInit();
            cutscene.PlayCutscene();
            State.ChangeMinigameState(StateType.Initialized);
        }

        protected virtual void OnGameInit()
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
        }
        #endregion

        #region Release
        public void Release() 
        {
            if (IsHost == false)
            {
                Debug.LogError("Lifecycle methods should only be called by the host.");
                return;
            }

            OnReleaseEvent?.Broadcast();
        }

        private void HandleGameRelease()
        {
            OnGameRelease();
            State.ChangeMinigameState(StateType.Released);
        }

        protected virtual void OnGameRelease()
        {
        }
        #endregion

        #region Start
        public void StartGame()
        {
            if (IsHost == false)
            {
                Debug.LogError("Lifecycle methods should only be called by the host.");
                return;
            }

            OnStartEvent?.Broadcast();
        }

        private void HandleGameStart()
        {
            OnGameStart();
            State.ChangeMinigameState(StateType.Playing);
        }

        protected virtual void OnGameStart()
        {
            InputManager.ChangeInputMap(InputMapType.Play);
            GameManager.Instance.CursorActive = false;

            cutscenePanel.Display(false);

            minigamePanel.Init(this);
            minigamePanel.Display(true);
        }
        #endregion

        #region Finish
        public void FinishGame()
        {
            if (IsHost == false)
            {
                Debug.LogError("Lifecycle methods should only be called by the host.");
                return;
            }

            OnFinishEvent?.Broadcast();
        }

        private void HandleGameFinish()
        {
            OnGameFinish();
            state.ChangeMinigameState(StateType.Finished);
            StartCoroutine(this.DelayCoroutine(minigameData.ResultPostponeTime, () => {
                cycle.DisplayResult();
            }));
        }

        protected virtual void OnGameFinish()
        {
            InputManager.ChangeInputMap(InputMapType.UI);
            GameManager.Instance.CursorActive = true;
        }
        #endregion

        #region HandleState
        private void HandleStateSynced(StateType stateType)
        {
            switch(stateType)
            {
                case StateType.Spawned:
                    Init();
                    break;
                case StateType.CutsceneFinished:
                    StartGame();
                    break;
                case StateType.Released:
                    MinigameManager.Instance.ReleaseMinigame();
                    break;
            }
        }
        #endregion

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            state.Release();

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
