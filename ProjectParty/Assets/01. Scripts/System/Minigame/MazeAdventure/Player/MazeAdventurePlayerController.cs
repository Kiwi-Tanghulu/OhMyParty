using OMG.FSM;
using OMG.Player;
using UnityEngine;
using UnityEngine.Events;
using OMG.Extensions;
using Unity.Netcode;
using OMG.Player.FSM;

namespace OMG.Minigames.MazeAdventure
{
    public class MazeAdventurePlayerController : PlayerController, IInvisibility
    {
        [SerializeField] private ItemSystem itemSystem;
        [SerializeField] private UnityEvent dieEvent;
        [SerializeField] private PlayerOutLine playerOutLine;
        [SerializeField] private MazeAdventurePlayerVisual mazeAdventurePlayerVisual;
        private bool isInvisibil;
        public UnityEvent OnInvisibil;
        public bool IsInvisibil => isInvisibil;


        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            //playerOutLine = Visual.SkinSelector.CurrentSkin.GetComponent<PlayerOutLine>();
            //int index = MinigameManager.Instance.CurrentMinigame.PlayerDatas.Find(out PlayerData foundPlayer, x => x.clientID == OwnerClientId);
            //playerOutLine.SettingOutLine(index);
            itemSystem.Init(transform);
            isInvisibil = false;
        }

        public void PlayerDead()
        {
            if (!IsOwner)
                return;

            GetComponent<CharacterMovement>().SetMoveSpeed(0f);
            GetCompo<CharacterFSM>().ChangeState(typeof(DieState));
            PlayerDeadServerRpc();
        }

        [ServerRpc]
        private void PlayerDeadServerRpc()
        {
            DeathmatchCycle cycle = MinigameManager.Instance.CurrentMinigame.Cycle as DeathmatchCycle; ;
            cycle.SetPlayerDead(OwnerClientId);
            dieEvent?.Invoke();
        }

        #region Invisibil
        [ServerRpc]
        public void EnterInvisibilServerRpc()
        { 
            EnterInvisibilClientRpc();
        }

        [ClientRpc]
        private void EnterInvisibilClientRpc()
        {
            isInvisibil = true;
            OnInvisibil?.Invoke();
            mazeAdventurePlayerVisual.ChangeColorInvisibility();
        }

        [ServerRpc]
        public void ExitInvisibilServerRpc()
        {
            ExitInvisibilClientRpc();
        }

        [ClientRpc]
        private void ExitInvisibilClientRpc()
        {
            if (isInvisibil == false) return;
            isInvisibil = false;
            mazeAdventurePlayerVisual.ChangeColorDefault();
        }

        public void EnterInvisibil()
        {
            if (IsHost)
            {
                EnterInvisibilClientRpc();
            }
            else
            {
                EnterInvisibilServerRpc();
            }
        }

        public void ExitInvisibil()
        {
            if (IsHost)
            {
                ExitInvisibilClientRpc();
            }
            else
            {
                if(IsOwner)
                {
                    ExitInvisibilServerRpc();
                }
            }
        }

        #endregion
    }
}
