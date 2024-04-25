using OMG.Minigames;
using OMG.Minigames.BikeRace;
using OMG.Player.FSM;
using Unity.Netcode;
using UnityEngine;

namespace OMG.Player
{
    public class BikeRacePlayerController : PlayerController
    {
        private BikeRace bikeRace;

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            bikeRace = MinigameManager.Instance.CurrentMinigame as BikeRace;

            if (IsHost)
            {
                bikeRace.OnStartGame += Minigame_OnStartGame;
            }

            bikeRace.OnPlayerGoal += Minigame_OnPlayerGoal;
        }

        

        private void Minigame_OnStartGame()
        {
            StateMachine.ChangeState(typeof(BikeMoveState));
        }

        private void Minigame_OnPlayerGoal(int index)
        {
            bikeRace.Players[index].TryGet(out NetworkObject networkObject);

            if(networkObject.OwnerClientId == OwnerClientId)
            {
                Goal();
            }
        }

        private void Goal()
        {
            gameObject.SetActive(false);
        }
    }
}