using OMG.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class BalanceGimmick : Gimmick
    {
        [SerializeField] private float additiveRotateSpeed;
        [SerializeField] private float returnSpeed;
        [SerializeField] private float returnDelayTime = 1f;
        private bool isReturn;
        private Coroutine returnCo;
        private WaitForSeconds wfs;

        private List<Transform> ridingPlayers;

        private float rotateSpeed;

        private void Awake()
        {
            ridingPlayers = new List<Transform>();
            wfs = new WaitForSeconds(returnDelayTime);
        }

        private void Update()
        {
            rotateSpeed = 0;
            for (int i = 0; i < ridingPlayers.Count; i++)
            {
                rotateSpeed += additiveRotateSpeed *
                    Mathf.Sign(ridingPlayers[i].position.z - transform.position.z);
            }

            Rotate();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ridingPlayers.Add(collision.transform);
                isReturn = false;
                if(returnCo != null)
                {
                    StopCoroutine(returnCo);
                    returnCo = null;
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                ridingPlayers.Remove(collision.transform);
                if (ridingPlayers.Count == 0 && returnCo == null)
                {
                    returnCo = StartCoroutine(ReturnCo());
                }
            }
        }

        private void Rotate()
        {
            if(isReturn)
            {
                if (ridingPlayers.Count == 0)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation,
                        Quaternion.Euler(0, 0, 0), returnSpeed * Time.deltaTime);
                    return;
                }
            }

            transform.Rotate(transform.right, rotateSpeed * Time.deltaTime);
        }

        private IEnumerator ReturnCo()
        {
            yield return wfs;

            isReturn = true;
        }

        protected override void Execute()
        {
        }

        protected override bool IsExecutable()
        {
            return true;
        }
    }
}