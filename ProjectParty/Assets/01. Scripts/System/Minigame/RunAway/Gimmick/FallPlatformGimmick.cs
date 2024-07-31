using OMG.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class FallPlatformGimmick : Gimmick
    {
        [SerializeField] private float fallDelay;
        [SerializeField] private float resetDelay;
        [SerializeField] private float acceleration;
        private float fallSpeed;

        private Vector3 originPos;

        private bool isFall;

        private void Awake()
        {
            originPos = transform.localPosition;

            isFall = false;
        }

        private void Update()
        {
            if(isFall)
                Fall();
        }

        private void Fall()
        {
            fallSpeed += acceleration * Time.deltaTime;

            transform.position += fallSpeed * Vector3.down * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                if(isFall == false)
                    StartCoroutine(FallCo());
            }
        }

        private IEnumerator FallCo()
        {
            yield return new WaitForSeconds(fallDelay);
            isFall = true;

            yield return new WaitForSeconds(resetDelay);
            isFall = false;
            fallSpeed = 0f;
            transform.localPosition = originPos;
        }

        protected override bool IsExecutable()
        {
            return true;
        }
    }
}
