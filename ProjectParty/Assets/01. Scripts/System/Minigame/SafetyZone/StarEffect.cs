using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class StarEffect : MonoBehaviour
    {
        [SerializeField] private float dizzyStarSpeed;
        public void StartEffect()
        {
            StartCoroutine(DizzyStar());
        }

        private IEnumerator DizzyStar()
        {
            float rotationY = 0;
            while (true)
            {
                rotationY += Time.deltaTime * dizzyStarSpeed;
                transform.Rotate(new Vector3(0, rotationY, 0));
                yield return null;
            }
        }
    }
}
