using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class TaggerVisual : MonoBehaviour
    {
        [SerializeField] private float dissolveTime;
        private Material material;
        private void Awake()
        {
            material = GetComponent<Renderer>().material;
            material.SetFloat("Dissolve", 0);
        }

        public void OnDissolve()
        {
            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve()
        {
            float curTime = 0;
            while(curTime >= dissolveTime)
            {
                curTime += Time.deltaTime;
                material.SetFloat("Dissolve",curTime / dissolveTime);
                yield return null;
            }
        }
    }
}
