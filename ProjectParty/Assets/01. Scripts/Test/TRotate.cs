using UnityEngine;

namespace OMG.Test
{
    public class TRotate : MonoBehaviour
    {
        private void Update()
        {
            transform.RotateAround(transform.position, Vector3.up, 10f * Time.deltaTime);
        }
    }
}
