using OMG.Extensions;
using OMG.FSM;
using OMG.Lobbies;
using OMG.Player.FSM;
using OMG.UI;
using UnityEngine;

namespace OMG.Player
{
    public class CalculateScoreState : PlayerFSMState
    {
        [SerializeField] private ScoreText scoreText;
        private CharacterMovement movement;

        private Transform standingTrm;

        public override void InitState(CharacterFSM brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<CharacterMovement>();

            Lobby.Current.GetLobbyComponent<LobbyCutSceneComponent>().
                CutSceneEvents[LobbyCutSceneState.EndFinish] += LobbyCutSscene_OnEndFinish;

            scoreText.GetComponent<AnimationEvent>().OnEndEvent.AddListener(CalculateScoreState_OnEndEvent);
        }

        public override void EnterState()
        {
            base.EnterState();

            standingTrm =
                Lobby.Current.GetLobbyComponent<LobbySkinComponent>().
                Skin.PlayerCalcScoreStandingPoint.GetStandingPoint(player.OwnerClientId);

            movement.Teleport(standingTrm.position, standingTrm.rotation);
        }

        private void LobbyCutSscene_OnEndFinish()
        {
            int index = Lobby.Current.PlayerDatas.IndexOf(i => i.ClientID == player.OwnerClientId);
            scoreText.SetScore(Lobby.Current.PlayerDatas[index].Score);
            scoreText.Show();
        }

        private void CalculateScoreState_OnEndEvent()
        {
            if(player.IsOwner)
                brain.ChangeState(brain.DefaultState);
        }

        private void OnDestroy()
        {
            Lobby.Current.GetLobbyComponent<LobbyCutSceneComponent>().
                CutSceneEvents[LobbyCutSceneState.EndFinish] -= LobbyCutSscene_OnEndFinish;
        }
    }
}
