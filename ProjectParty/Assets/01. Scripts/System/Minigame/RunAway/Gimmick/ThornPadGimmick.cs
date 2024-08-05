using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class ThornPadGimmick : Gimmick
    {
        [SerializeField] private float upYPos;
        [SerializeField] private float upTime;
        [SerializeField] private float downYPos;
        [SerializeField] private float downTime;
        [SerializeField] private float executeTime;
        [SerializeField] private float executeDelayTime;

        [Space]
        [SerializeField] private float hitPower;

        private Sequence seq;

        private WaitForSeconds wfs;

        private void Start()
        {
            seq = DOTween.Sequence();
            seq.Append(transform.DOLocalMoveY(upYPos, upTime));
            seq.AppendInterval(executeTime);
            seq.Append(transform.DOLocalMoveY(downYPos, downTime));
            seq.AppendInterval(executeDelayTime);
            seq.SetAutoKill(false);
            seq.SetLoops(-1, LoopType.Restart);
            seq.Play();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                if(collision.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
                {
                    damageable.OnDamaged(hitPower, transform,
                        collision.GetContact(0).point, HitEffectType.Stun, default, Vector3.up);
                }
            }
        }

        //protected override void Execute()
        //{
        //    base.Execute();

        //    seq.Restart();
        //}

        //private IEnumerator ExecuteCo()
        //{
        //    while(true)
        //    {
        //        yield return wfs;

        //        Execute();
        //    }
        //}

        protected override bool IsExecutable()
        {
            return true;
        }
    }
}