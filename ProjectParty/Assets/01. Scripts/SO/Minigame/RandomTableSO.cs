using System.Linq;
using UnityEngine;

namespace OMG.Minigames.Utility
{
    [CreateAssetMenu(menuName = "SO/Minigame/RandomTable")]
    public class RandomTableSO : ScriptableObject
    {
        [System.Serializable]
        public class Slot
        {
            public float rate;
            public GameObject prefab;
        }

        [SerializeField] Slot[] table = null;
        private float[] weights = null;

        private void OnEnable()
        {
            weights = table.Select(i => i.rate).ToArray();
        }

        private void OnValidate()
        {
            weights = table.Select(i => i.rate).ToArray();
        }

        public GameObject GetObject()
        {
            int index = GetIndexByWeight();
            return table[index].prefab;
        }

        private int GetIndexByWeight()
        {
            float sum = weights.Sum();
            float weight = Random.Range(0f, sum);
            float nesting = 0f;

            for (int i = 0; i < weights.Length; i++)
            {
                float delta = nesting + weights[i];
                if (nesting <= weight && weight < delta)
                    return i;
                else
                    nesting = delta;
            }

            return 0;
        }
    }
}
