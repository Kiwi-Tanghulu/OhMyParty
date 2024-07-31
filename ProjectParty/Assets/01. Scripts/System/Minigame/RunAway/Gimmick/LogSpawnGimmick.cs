using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class LogSpawnGimmick : Gimmick
    {
        [SerializeField] private LogGimmick logPrefab;
        [SerializeField] private Transform logSpawnPoint;
        [SerializeField] private float logSpawnDelay = 3f;
        private WaitForSeconds wfs;

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
            Instantiate(logPrefab, logSpawnPoint.position, logSpawnPoint.rotation, transform);
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
    }
}