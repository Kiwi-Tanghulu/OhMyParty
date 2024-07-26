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
        public NetworkEvent OnActionNetworkEvent = new NetworkEvent("OnActionNetworkEvent");

        private ExtendedAnimator anim;


        public override void InitState(CharacterFSM brain)
        {
            base.InitState(brain);

            anim = player.transform.Find("Visual").GetComponent<ExtendedAnimator>();

            if(brain.Controller.IsSpawned)
            {
                OnActionNetworkEvent.Register(player.GetComponent<NetworkObject>());
            }
        }

        public override void EnterState()
        {
            base.EnterState();

            anim.AnimEvent.OnPlayingEvent += DoAction;

            anim.SetLayerWeight(AnimatorLayerType.Upper, 1, true, 0.1f);
        }

        public override void ExitState()
        {
            base.ExitState();

            anim.AnimEvent.OnPlayingEvent -= DoAction;

            anim.SetLayerWeight(AnimatorLayerType.Upper, 0, true, 0.1f);
        }

        public virtual void DoAction()
        {
            OnActionEvent?.Invoke();
            brain.Controller.InvokeNetworkEvent(OnActionNetworkEvent);
        }
    }
}
