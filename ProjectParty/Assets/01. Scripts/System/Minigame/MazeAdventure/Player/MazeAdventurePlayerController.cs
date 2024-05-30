using OMG.FSM;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using OMG.Extensions;
namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventurePlayerController : PlayerController
    {
        [SerializeField] private ItemSystem itemSystem;
        [SerializeField] private FSMState dieState;
        [SerializeField] private UnityEvent dieEvent;
        [SerializeField] private PlayerOutLine playerOutLine;
        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            int index = MinigameManager.Instance.CurrentMinigame.PlayerDatas.Find(out PlayerData foundPlayer, x => x.clientID == OwnerClientId);
            playerOutLine.SettingOutLine(index);
            itemSystem.Init(transform);  
        }

        public void PlayerDead()
        {
            dieEvent?.Invoke();
            StateMachine.ChangeState(dieState);
        }
    }
}
