using OMG.FSM;
using UnityEngine.Events;
using Unity.Netcode;
using UnityEngine;
using OMG.NetworkEvents;

using NetworkEvent = OMG.NetworkEvents.NetworkEvent;
using OMG.Lobbies;

namespace OMG.Player.FSM
{
    public class ActionState : PlayerFSMState
    {
        public UnityEvent OnActionEvent;

        private ExtendedAnimator anim;

        private NetworkEvent onAttackNetworkEvent = new NetworkEvent("DoActionEvent");

        public override void InitState(CharacterFSM brain)
        {
            base.InitState(brain);

            anim = player.transform.Find("Visual").GetComponent<ExtendedAnimator>();
        }

        public override void NetworkInit()
        {
            base.NetworkInit();

            onAttackNetworkEvent.AddListener(DoActionNetworkEvent);

            onAttackNetworkEvent.Register(player.GetComponent<NetworkObject>());
        }

        public override void EnterState()
        {
            base.EnterState();

            anim.AnimEvent.OnPlayingEvent += InvokeDoAction;

            anim.SetLayerWeight(AnimatorLayerType.Upper, 1, true, 0.1f);
        }

        public override void ExitState()
        {
            base.ExitState();

            anim.AnimEvent.OnPlayingEvent -= InvokeDoAction;

            anim.SetLayerWeight(AnimatorLayerType.Upper, 0, true, 0.1f);
        }


        private void InvokeDoAction()
        {
            if(brain.IsNetworkInit)
            {
                onAttackNetworkEvent.Alert();
            }
            else
            {
                DoAction();
            }
        }

        private void DoActionNetworkEvent(NoneParams param)
        {
            DoAction();
        }

        public virtual void DoAction()
        {
            OnActionEvent?.Invoke();
        }
    }
}
