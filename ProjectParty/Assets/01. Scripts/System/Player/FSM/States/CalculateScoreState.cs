using OMG.FSM;
using OMG.Lobbies;
using OMG.Minigames.MazeAdventure;
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

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            movement = player.GetComponent<CharacterMovement>();

            Lobby.Current.GetLobbyComponent<LobbyCutSceneComponent>().
                CutSceneEvents[LobbyCutSceneState.EndFinish] += LobbyCutSscene_OnEndFinish;

            scoreText.GetComponent<AnimationEvent>().OnEndEvent += CalculateScoreState_OnEndEvent;
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
            scoreText.SetScore(Lobby.Current.PlayerDatas[(int)player.OwnerClientId].Score);
            scoreText.Show();
        }

        private void CalculateScoreState_OnEndEvent()
        {
            brain.ChangeState(brain.DefaultState);
        }

        private void OnDestroy()
        {
            Lobby.Current.GetLobbyComponent<LobbyCutSceneComponent>().
                CutSceneEvents[LobbyCutSceneState.EndFinish] -= LobbyCutSscene_OnEndFinish;
        }
    }
}
