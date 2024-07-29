using OMG.Extensions;
using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class TreppolineGimmick : Gimmick
    {
        [SerializeField] private Transform footboardTrm;
        [SerializeField] private float readyPos;
        [SerializeField] private float executePos;
        [SerializeField] private float readyDelayTime;
        private bool isReady;

        [Space]
        [SerializeField] private float effectPower;

        private PlayerMovement target;

        private void Start()
        {
            Ready();
        }

        private void OnCollisionEnter(Collision collision)
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
            isReady = true;

            footboardTrm.localPosition = readyPos * Vector3.up;
        }

        protected override void Execute()
        {
            base.Execute();

            target.Jump(effectPower);
            isReady = false;
            footboardTrm.localPosition = executePos * Vector3.up;

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