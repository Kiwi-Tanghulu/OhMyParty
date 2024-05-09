using UnityEngine;

namespace OMG.Extensions
{
    public static class TransformExtensions
    {
	    public static bool InnerDistance(this Transform left, Transform right, float distance)
        {
            float sqrDistance = (left.position - right.position).sqrMagnitude;
            return sqrDistance < (distance * distance);
        }

        public static Transform FindFromAll(this Transform parent, string childName)
        {
            Transform result = null;

            foreach(Transform child in parent)
            {
                if (child.name == childName)
                    result = child;
                else
                    result = child.FindFromAll(childName);

                if (result != null)
                    return result;
            }

            return null;
        }
    }   
}