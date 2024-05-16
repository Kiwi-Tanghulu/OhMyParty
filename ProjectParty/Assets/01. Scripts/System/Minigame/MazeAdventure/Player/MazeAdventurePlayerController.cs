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
        [SerializeField] private FSMState dieState;
        [SerializeField] private UnityEvent dieEvent;

        public override void Init(ulong ownerId)
        {
            base.Init(ownerId);

            int index = MinigameManager.Instance.CurrentMinigame.PlayerDatas.Find(out PlayerData foundPlayer, x => x.clientID == ownerId);

            transform.Find("Visual").Find("Skin").GetComponent<PlayerOutLine>().SettingOutLine(index);
        }
        public void PlayerDead()
        {
            dieEvent?.Invoke();
            StateMachine.ChangeState(dieState);
        }
    }
}
