using UnityEngine;

namespace OMG.Extensions
{
    public static class VectorExtensions
    {
        public static Vector3 GetRandom(this Vector3 left, Vector3 right)
        {
            float posX = Random.Range(left.x, right.x);
            float posY = Random.Range(left.y, right.y);
            float posZ = Random.Range(left.z, right.z);

            return new Vector3(posX, posY, posZ);
        }
    }
}
