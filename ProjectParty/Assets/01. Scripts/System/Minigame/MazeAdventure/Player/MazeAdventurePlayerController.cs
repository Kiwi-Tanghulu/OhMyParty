using OMG.FSM;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OMG.Extensions;
namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventurePlayerController : PlayerController, IInvisibility
    {
        [SerializeField] private ItemSystem itemSystem;
        [SerializeField] private FSMState dieState;
        [SerializeField] private UnityEvent dieEvent;
        [SerializeField] private PlayerOutLine playerOutLine;
        [SerializeField] private MazeAdventurePlayerVisual mazeAdventurePlayerVisual;
        private bool isInvisibil;
        public bool IsInvisibil => isInvisibil;


        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            int index = MinigameManager.Instance.CurrentMinigame.PlayerDatas.Find(out PlayerData foundPlayer, x => x.clientID == OwnerClientId);
            playerOutLine.SettingOutLine(index);
            itemSystem.Init(transform);
            isInvisibil = false;
        }

        public void PlayerDead()
        {
            dieEvent?.Invoke();
            StateMachine.ChangeState(dieState);
        }
        public void EnterInvisibil()
        {
            isInvisibil = true;
            mazeAdventurePlayerVisual.ChangeColorInvisibility();
        }
        public void ExitInvisibil()
        {
            isInvisibil = false;
            mazeAdventurePlayerVisual.ChangeColorDefault();
        }
    }
}
