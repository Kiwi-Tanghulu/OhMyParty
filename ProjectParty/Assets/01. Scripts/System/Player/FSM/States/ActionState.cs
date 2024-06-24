using OMG.FSM;
using UnityEngine.Events;
using Unity.Netcode;

namespace OMG.Player.FSM
{
    public class ActionState : PlayerFSMState
    {
        public UnityEvent OnActionEvent;

        private ExtendedAnimator anim;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            anim = player.transform.Find("Visual").GetComponent<ExtendedAnimator>();
        }

        public override void EnterState()
        {
            base.EnterState();

            anim.AnimEvent.OnPlayingEvent += DoActionServerRpc;

            anim.SetLayerWeight(AnimatorLayerType.Upper, 1, true, 0.1f);
        }

        public override void ExitState()
        {
            base.ExitState();

            anim.AnimEvent.OnPlayingEvent -= DoActionServerRpc;

            anim.SetLayerWeight(AnimatorLayerType.Upper, 0, true, 0.1f);
        }

        [ServerRpc]
        private void DoActionServerRpc()
        {
            DoAction();
        }

        protected virtual void DoAction()
        {
            OnActionEvent?.Invoke();
        }
    }
}
