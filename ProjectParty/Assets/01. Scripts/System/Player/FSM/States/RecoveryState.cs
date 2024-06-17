using System.Collections;
using UnityEngine;
using OMG.FSM;
using OMG.Skins;
using DG.Tweening;

namespace OMG.Player.FSM
{
    public class RecoveryState : PlayerFSMState
    {
        private ExtendedAnimator anim;
        private PlayerHealth health;

        [SerializeField] private float playerHitableDelayTime = 1f;

        private WaitForSeconds wfs;

        private CharacterSkin playerSkin;
        private readonly string matColorID = "_BaseColor";

        private Sequence twinkleTween;

        public override void InitState(FSMBrain brain)
        {
            base.InitState(brain);

            anim = player.Animator;
            health = player.GetComponent<PlayerHealth>();
            playerSkin = player.Visual.SkinSelector.CurrentSkin as CharacterSkin;

            wfs = new WaitForSeconds(playerHitableDelayTime);
        }

        public override void EnterState()
        {
            base.EnterState();

            float twinkleTweenTime = playerHitableDelayTime / 4f;
            twinkleTween = DOTween.Sequence();
            twinkleTween.Append(playerSkin.Mat.DOFade(0f, twinkleTweenTime));
            twinkleTween.Append(playerSkin.Mat.DOFade(1f, twinkleTweenTime));
            twinkleTween.Append(playerSkin.Mat.DOFade(0f, twinkleTweenTime));
            twinkleTween.Append(playerSkin.Mat.DOFade(1f, twinkleTweenTime));

            anim.AnimEvent.OnEndEvent += AnimEvent_OnEndEvent;

            health.Hitable = false;
            health.PlayerHitable = false;
        }

        public override void ExitState()
        {
            base.ExitState();

            anim.AnimEvent.OnEndEvent -= AnimEvent_OnEndEvent;

            health.Hitable = true;
            player.StartCoroutine(HitableDelayCo());

            twinkleTween.Restart();
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
        }
    }
}