using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public enum TaggerMaterial
    {
        Basic,
        Chase
    }
    public class TaggerVisual : NetworkBehaviour
    {
        [SerializeField] private Material[] taggerMat;
        [SerializeField] private float dissolveTime;
        private Material material;
        private SkinnedMeshRenderer skinnedMeshRenderer;
        private void Awake()
        {
            material = GetComponent<Renderer>().material;
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }

        public void OnDissolve()
        {
            StartCoroutine(Dissolve());
        }

        public void Invisible()
        {
            material.SetFloat("_Dissolve", 0);
        }

        private IEnumerator Dissolve()
        {
            material.SetFloat("_Dissolve", 0);
            float curTime = 0;
            while(curTime <= dissolveTime)
            {
                curTime += Time.deltaTime;
                material.SetFloat("_Dissolve",curTime / dissolveTime);
                yield return null;
            }
            material.SetFloat("_Dissolve", 1.1f);
        }

        [ClientRpc]
        public void ChangeMaterialClientRpc(int index)
        {
            skinnedMeshRenderer.material = taggerMat[index];
        }
    }
}
