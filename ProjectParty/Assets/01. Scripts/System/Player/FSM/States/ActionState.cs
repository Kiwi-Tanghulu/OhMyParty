using OMG.FSM;
using UnityEngine.Events;
using Unity.Netcode;
using UnityEngine;
using OMG.NetworkEvents;

using NetworkEvent = OMG.NetworkEvents.NetworkEvent;

namespace OMG.Player.FSM
{
    public class ActionState : PlayerFSMState
    {
        public UnityEvent OnActionEvent;

        private ExtendedAnimator anim;

        private NetworkEvent onAttackNetworkEvent = new NetworkEvent("DoActionEvent");

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            anim = player.transform.Find("Visual").GetComponent<ExtendedAnimator>();

            onAttackNetworkEvent.AddListener(DoAction);

            onAttackNetworkEvent.Register(player.GetComponent<NetworkObject>());
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


        private void DoActionServerRpc()
        {
            onAttackNetworkEvent.Alert();
        }

        private void DoAction(NoneParams param)
        {
            DoAction();
        }

        public virtual void DoAction()
        {
            OnActionEvent?.Invoke();
        }
    }
}
