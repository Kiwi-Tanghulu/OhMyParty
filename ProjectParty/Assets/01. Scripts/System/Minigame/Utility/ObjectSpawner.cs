using OMG.Extensions;
using UnityEngine;

namespace OMG.Minigames.Utility
{
    public class ObjectSpawner : MonoBehaviour
    {
        [SerializeField] RandomTableSO table = null;

        [SerializeField] Transform minPos = null;
        [SerializeField] Transform maxPos = null;

        public virtual GameObject SpawnObject()
        {
            GameObject prefab = table.GetObject();
            Vector3 position = VectorExtensions.GetRandom(minPos.position, maxPos.position);
            GameObject instance = Instantiate(prefab, position, Quaternion.identity);
            
            return instance;
        }
    }
}
