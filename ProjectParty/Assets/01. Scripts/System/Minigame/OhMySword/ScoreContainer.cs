using System.Collections.Generic;
using OMG.Extensions;
using UnityEngine;

namespace OMG.Minigames.OhMySword
{
    public class ScoreContainer : MonoBehaviour
    {
        [SerializeField] XPObject[] xpPrefabs = null;
        [SerializeField] float xpSpawnOutRadius = 3f;
        [SerializeField] float xpSpawnInRadius = 1.5f;
        private int xpAmount = 0;

        private LinkedList<XPObject> scoreContainer = null;

        public void Init(int xp, LinkedList<XPObject> container)
        {
            xpAmount = xp;
            scoreContainer = container;
        }

        public void GenerateXP()
        {
            xpAmount.ForEachDigit((digit, number, index) =>
            {
                float distance = Random.Range(xpSpawnInRadius, xpSpawnOutRadius);
                Vector2 randomPosition = Random.insideUnitCircle.normalized * distance;
                Vector3 position = new Vector3(randomPosition.x, 0, randomPosition.y);
                position += transform.position;

                XPObject xpPrefab = xpPrefabs[(int)Mathf.Log10(digit)];
                XPObject xp = Instantiate(xpPrefab, transform.position, Quaternion.identity);
                xp.NetworkObject.Spawn(true);
                xp.Init(position);

                scoreContainer.AddLast(xp);
            });
        }
    }
}
