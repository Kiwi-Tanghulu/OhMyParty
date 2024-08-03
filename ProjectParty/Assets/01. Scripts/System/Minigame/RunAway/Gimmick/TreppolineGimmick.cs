using OMG.Extensions;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements.Experimental;

namespace OMG.Minigames
{
    public class TreppolineGimmick : Gimmick
    {
        [SerializeField] private Transform footboardTrm;
        [SerializeField] private float readyPos;
        [SerializeField] private float executePos;
        [SerializeField] private float executeTime;
        [SerializeField] private float readyTime;
        [SerializeField] private float readyDelayTime;
        private bool isReady;

        [Space]
        [SerializeField] private float effectPower;

        private PlayerMovement target;

        private void Start()
        {
            Ready();
        }

        private void OnCollisionStay(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                target = collision.transform.GetComponent<PlayerMovement>();

                if(IsExecutable())
                {
                    Execute();    
                }
            }
        }

        private void Ready()
        {
            footboardTrm.DOLocalMoveY(readyPos, readyTime).SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    isReady = true;
                });
        }

        protected override void Execute()
        {
            base.Execute();

            target.Jump(effectPower);
            isReady = false;
            footboardTrm.DOLocalMoveY(executePos, executeTime).SetEase(Ease.Linear);

            StartCoroutine(ReadyDelay());
        }

        protected override bool IsExecutable()
        {
            return isReady;
        }

        private IEnumerator ReadyDelay()
        {
            yield return new WaitForSeconds(readyDelayTime);

            Ready();
        }
    }
}