using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class StarEffect : MonoBehaviour
    {
        [SerializeField] private float dizzyStarSpeed;
        private List<GameObject> stars = null;

        private void Awake()
        {
            stars = new List<GameObject>();
            foreach (Transform t in transform) 
            {
                stars.Add(t.gameObject);
                t.gameObject.SetActive(false);
            }

        }
        public void StartEffect()
        {
            foreach (GameObject starObj in stars)
            {
                starObj.SetActive(true);
            }
            StartCoroutine(DizzyStar());
        }

        public void StopEffect()
        {
            foreach(GameObject starObj in stars)
            {
                starObj.SetActive(false);
            }
            StopAllCoroutines();
        }

        private IEnumerator DizzyStar()
        {
            while (true)
            {
                transform.Rotate(new Vector3(0, Time.deltaTime * dizzyStarSpeed, 0));
                yield return null;
            }
        }
    }
}
