using OMG.Extensions;
using OMG.Inputs;
using OMG.UI;
using OMG.UI.Minigames;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames
{
    public abstract class Minigame : NetworkBehaviour
    {
        [SerializeField] MinigameSO minigameData = null;
        public MinigameSO MinigameData => minigameData;

        public UnityEvent OnStartedEvent = new UnityEvent();
        public UnityEvent OnFinishedEvent = new UnityEvent();

        protected NetworkList<PlayerData> playerDatas = null;
        public NetworkList<PlayerData> PlayerDatas => playerDatas;

        protected MinigamePanel minigamePanel = null;
        public MinigamePanel MinigamePanel => minigamePanel;

        protected CutscenePanel cutscenePanel = null;
        public CutscenePanel CutscenePanel => cutscenePanel;

        protected MinigameCycle cycle = null;
        public MinigameCycle Cycle => cycle;

        protected virtual void Awake()
        {
            playerDatas = new NetworkList<PlayerData>();
            cycle = GetComponent<MinigameCycle>();

            cutscenePanel = DEFINE.MinigameCanvas?.transform.Find("CutscenePanel").GetComponent<CutscenePanel>();
            minigamePanel = DEFINE.MinigameCanvas?.transform.Find("MinigamePanel").GetComponent<MinigamePanel>();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            MinigameManager.Instance.CurrentMinigame = this;
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

            StartIntro();
        }

        /// <summary>
        /// Only Host Could Call this Method
        /// </summary>
        public virtual void Release() 
        {
        }

        public virtual void StartIntro()
        {
            StartCoroutine(this.DelayCoroutine(minigameData.IntroPostponeTime, () => {
                cycle.PlayIntro();
            }));
        }

        public virtual void StartOutro()
        {
            StartCoroutine(this.DelayCoroutine(minigameData.OutroPostponeTime, () => {
                cycle.PlayOutro();
            }));
        }

        public virtual void StartGame()
        { 
            InputManager.ChangeInputMap(InputMapType.Play);
            GameManager.Instance.CursorActive = false;
            minigamePanel.Init(this);
            minigamePanel.Display(true);
            OnStartedEvent?.Invoke();
        }

        public virtual void FinishGame() 
        {
            InputManager.ChangeInputMap(InputMapType.UI);
            GameManager.Instance.CursorActive = true;
            Debug.Log("1");
            OnFinishedEvent?.Invoke();
            StartOutro();
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

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
