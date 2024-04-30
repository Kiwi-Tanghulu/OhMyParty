using OMG.Extensions;
using OMG.Input;
using OMG.UI.Minigames;
using System;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Minigames
{
    public abstract class Minigame : NetworkBehaviour
    {
        [SerializeField] MinigameSO minigameData = null;
        public MinigameSO MinigameData => minigameData;

        protected NetworkList<PlayerData> playerDatas = null;
        public NetworkList<PlayerData> PlayerDatas => playerDatas;

        protected MinigameUI minigameUI = null;
        public MinigameUI MinigameUI => minigameUI;

        protected MinigameCycle cycle = null;

        public event Action OnFinishGame;

        protected virtual void Awake()
        {
            playerDatas = new NetworkList<PlayerData>();
            cycle = GetComponent<MinigameCycle>();
            minigameUI = transform.Find("MinigameUI").GetComponent<MinigameUI>();
        }

        public override void OnNetworkSpawn()
        {
            MinigameManager.Instance.CurrentMinigame = this;
        }

        /// <summary>
        /// Only Host Could Call this Method
        /// </summary>  
        public virtual void Init(params ulong[] playerIDs)
        {
            for(int i = 0; i < playerIDs.Length; ++i)
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
        }

        public virtual void FinishGame() 
        {
            InputManager.ChangeInputMap(InputMapType.UI);
            OnFinishGame?.Invoke();
            StartOutro();
        }

        public virtual int CalculateScore(int origin) => origin;
    }
}
