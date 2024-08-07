using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class LogSpawnGimmick : Gimmick
    {
        [SerializeField] private LogGimmick logPrefab;
        [SerializeField] private float logSpawnDelay = 3f;
        private WaitForSeconds wfs;

        [Space]
        //[SerializeField] private ThreeDirectionAudioPlayer audioPlayer;
        [SerializeField] private AudioClip breakAudioClip;
        [SerializeField] private float maxDistance;

        private void Awake()
        {
            wfs = new WaitForSeconds(logSpawnDelay);
        }

        private void Start()
        {
            if (logSpawnDelay != 0)
                StartCoroutine(SpawnCo());
            else
                Debug.LogError("Log Spawn Delay is 0");
        }

        protected override void Execute()
        {
            base.Execute();
            LogGimmick log = Instantiate(logPrefab, transform.position, transform.rotation, transform);
            log.SetMoveDirection(transform.right);
            log.OnBreakEvent.AddListener(PlayLogBreakSound);
        }

        protected override bool IsExecutable()
        {
            return true;
        }

        private IEnumerator SpawnCo()
        {
            while(true)
            {
                yield return wfs;

                Execute();
            }
        }

        private void PlayLogBreakSound(Vector3 pos)
        {   
            //audioPlayer.PlayOneShot(breakAudioClip, pos, maxDistance);
        }
    }
}