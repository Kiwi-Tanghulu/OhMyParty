using System.Collections;
using UnityEngine;
using OMG.FSM;
using OMG.Skins;
using DG.Tweening;
using OMG.NetworkEvents;
using Unity.Netcode;

using NetworkEvent = OMG.NetworkEvents.NetworkEvent;

namespace OMG.Player.FSM
{
    public class RecoveryState : PlayerFSMState
    {
        private ExtendedAnimator anim;
        private PlayerHealth health;

        [SerializeField] private float playerHitableDelayTime = 1f;

        private WaitForSeconds wfs;

        private CharacterSkin playerSkin;

        private Sequence twinkleTween;

        private NetworkEvent onStartRecoveryEvent = new NetworkEvent("onStartRecoveryEvent");
        private NetworkEvent onEndRecoveryEvent = new NetworkEvent("onEndRecoveryEvent");

        public override void InitState(CharacterFSM brain)
        {
            base.InitState(brain);

            anim = player.GetCharacterComponent<PlayerVisual>().Anim;
            health = player.GetComponent<PlayerHealth>();
            
            playerSkin = player.GetCharacterComponent<PlayerVisual>().SkinSelector.CurrentSkin as CharacterSkin;
            
            wfs = new WaitForSeconds(playerHitableDelayTime);

            if(brain.Controller.IsSpawned)
            {
                onStartRecoveryEvent.AddListener(OnStartRecovery);
                onEndRecoveryEvent.AddListener(OnEndRecovery);

                onStartRecoveryEvent.Register(player.GetComponent<NetworkObject>());
                onEndRecoveryEvent.Register(player.GetComponent<NetworkObject>());
            }
        }

        public override void EnterState()
        {
            base.EnterState();

            anim.AnimEvent.OnEndEvent.AddListener(AnimEvent_OnEndEvent);

            health.Hitable = false;
            health.PlayerHitable = false;

            brain.Controller.InvokeNetworkEvent(onStartRecoveryEvent);
        }

        public override void ExitState()
        {
            base.ExitState();

            anim.AnimEvent.OnEndEvent.RemoveListener(AnimEvent_OnEndEvent);

            health.Hitable = true;

            brain.Controller.InvokeNetworkEvent(onEndRecoveryEvent);
        }

        private void AnimEvent_OnEndEvent()
        {
            ChangeDefaultState();
        }

        private void ChangeDefaultState()
        {
            if(player.IsServer)
            {
                brain.ChangeState(brain.DefaultState);
            }
        }

        private IEnumerator HitableDelayCo()
        {
            yield return wfs;

            health.PlayerHitable = true;
            twinkleTween.Pause();
            twinkleTween.Kill();

            playerSkin.Mat.DOFade(1f, 0f);
        }

        private void OnStartRecovery(NoneParams param)
        {
            if (playerSkin == null)
                playerSkin = player.GetCharacterComponent<PlayerVisual>().SkinSelector.CurrentSkin as CharacterSkin;

            float twinkleTweenTime = playerHitableDelayTime / 4f;
            twinkleTween = DOTween.Sequence();
            twinkleTween.Append(playerSkin.Mat.DOFade(0f, twinkleTweenTime));
            twinkleTween.Append(playerSkin.Mat.DOFade(1f, twinkleTweenTime));
            twinkleTween.SetLoops(-1);
            twinkleTween.Play();
        }

        private void OnEndRecovery(NoneParams param)
        {
            player.StartCoroutine(HitableDelayCo());
        }

        private void OnDestroy()
        {
            if (brain.Controller.IsSpawned)
            {
                onStartRecoveryEvent.Unregister();
                onEndRecoveryEvent.Unregister();
            }
        }
    }
}