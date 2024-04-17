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

        public static void MultipleEach(this Vector3 left, Vector3 right)
        {
            left.x *= right.x;
            left.y *= right.y;
            left.z *= right.z;
        }

        public static Vector3 GetMultipleEach(this Vector3 left, Vector3 right)
        {
            Vector3 value = Vector3.zero;
            value.x = left.x * right.x;
            value.y = left.x * right.y;
            value.z = left.z * right.z;

            return value;
        }

        public static void Abs(this Vector3 left)
        {
            left.x = Mathf.Abs(left.x);
            left.y = Mathf.Abs(left.y);
            left.z = Mathf.Abs(left.z);
        }

        public static Vector3 GetAbs(this Vector3 left)
        {
            Vector3 value = Vector3.zero;
            value.x = Mathf.Abs(left.x);
            value.y = Mathf.Abs(left.y);
            value.z = Mathf.Abs(left.z);
            
            return value;
        }
    }
}
