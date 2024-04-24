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

        #region Clamp
        public static Vector3 GetClampEach(this Vector3 left, Vector3 min, Vector3 max)
        {
            if (min.x > max.x)
                (min.x, max.x) = (max.x, min.x);
            if (min.y > max.y)
                (min.y, max.y) = (max.y, min.y);
            if (min.z > max.z)
                (min.z, max.z) = (max.z, min.z);

            Vector3 value = Vector3.zero;
            value.x = Mathf.Clamp(left.x, min.x, max.x);
            value.y = Mathf.Clamp(left.y, min.y, max.y);
            value.z = Mathf.Clamp(left.z, min.z, max.z);
            return value;
        }

        public static Vector3Int GetClampEach(this Vector3Int left, Vector3Int min, Vector3Int max)
        {
            if(min.x > max.x)
                (min.x, max.x) = (max.x, min.x);
            if(min.y > max.y)
                (min.y, max.y) = (max.y, min.y);
            if(min.z > max.z)
                (min.z, max.z) = (max.z, min.z);

            Vector3Int value = Vector3Int.zero;
            value.x = Mathf.Clamp(left.x, min.x, max.x);
            value.y = Mathf.Clamp(left.y, min.y, max.y);
            value.z = Mathf.Clamp(left.z, min.z, max.z);
            return value;
        }
        #endregion

        #region Abs
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

        public static void Abs(this Vector3Int left)
        {
            left.x = Mathf.Abs(left.x);
            left.y = Mathf.Abs(left.y);
            left.z = Mathf.Abs(left.z);
        }

        public static Vector3Int GetAbs(this Vector3Int left)
        {
            Vector3Int value = Vector3Int.zero;
            value.x = Mathf.Abs(left.x);
            value.y = Mathf.Abs(left.y);
            value.z = Mathf.Abs(left.z);
            
            return value;
        }
        #endregion
    }
}
