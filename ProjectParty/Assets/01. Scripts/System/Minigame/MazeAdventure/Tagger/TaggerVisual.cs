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
            Debug.Log(material);
        }

        public void OnDissolve()
        {
            StartCoroutine(Dissolve());
        }

        private IEnumerator Dissolve()
        {
            material.SetFloat("_Dissolve", 0);
            Debug.Log("µðÁ¹ºê½ÇÇà");
            float curTime = 0;
            while(curTime <= dissolveTime)
            {
                curTime += Time.deltaTime;
                material.SetFloat("_Dissolve",curTime / dissolveTime);
                yield return null;
            }
            material.SetFloat("_Dissolve", 1f);
        }
    }
}
