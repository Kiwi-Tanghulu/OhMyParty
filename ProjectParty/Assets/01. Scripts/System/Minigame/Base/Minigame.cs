using OMG.Extensions;
using OMG.Inputs;
using OMG.UI;
using OMG.UI.Minigames;
using System;
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

        protected MinigameUI minigameUI = null;
        public MinigameUI MinigameUI => minigameUI;

        protected MinigameCycle cycle = null;

        protected virtual void Awake()
        {
            playerDatas = new NetworkList<PlayerData>();
            cycle = GetComponent<MinigameCycle>();
            minigameUI = DEFINE.MinigameCanvas.GetComponent<MinigameUI>();
        }

        public override void OnNetworkSpawn()
        {
            MinigameManager.Instance.CurrentMinigame = this;

            Fade.Instance.FadeIn(3f, () =>
            {
                Time.timeScale = 0f;
            }, () =>
            {
                Time.timeScale = 1.0f;
            });
        }

        /// <summary>
        /// Only Host Could Call this Method
        /// </summary>  
        public virtual void Init(params ulong[] playerIDs)
        {
            for (int i = 0; i < playerIDs.Length; ++i)
                playerDatas.Add(new PlayerData(playerIDs[i]));
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
            OnStartedEvent?.Invoke();
        }

        public virtual void FinishGame() 
        {
            InputManager.ChangeInputMap(InputMapType.UI);
            OnFinishedEvent?.Invoke();
            StartOutro();
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();

            Fade.Instance.FadeIn(3f, () =>
            {
                Time.timeScale = 0f;
            }, () =>
            {
                Time.timeScale = 1.0f;
            });
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
