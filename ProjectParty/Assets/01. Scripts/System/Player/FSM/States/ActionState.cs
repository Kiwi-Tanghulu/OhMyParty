using OMG.FSM;
using UnityEngine.Events;
using Unity.Netcode;

using NetworkEvent = OMG.NetworkEvents.NetworkEvent;

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

            anim.AnimEvent.OnPlayingEvent.AddListener(DoAction);

            anim.SetLayerWeight(AnimatorLayerType.Upper, 1, true, 0.1f);
        }

        public override void ExitState()
        {
            base.ExitState();

            anim.AnimEvent.OnPlayingEvent.RemoveListener(DoAction);

            anim.SetLayerWeight(AnimatorLayerType.Upper, 0, true, 0.1f);
        }

        public virtual void DoAction()
        {
            OnActionEvent?.Invoke();
            brain.Controller.InvokeNetworkEvent(OnActionNetworkEvent);
        }

        private void OnDestroy()
        {
            if (brain.Controller.IsSpawned)
            {
                OnActionNetworkEvent.Unregister();
            }
        }
    }
}
