using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace OMG
{
    public class ReactiveMaterial : MonoBehaviour
    {
        [SerializeField] private LayerMask reactionLayer;
        [SerializeField] private int maxReactionCount;
        [SerializeField] private string propertyName;

        private float reactionDistacne;

        private List<Transform> reactionTargetTrmList;

        private Renderer render;

        private void Awake()
        {
            render = GetComponent<Renderer>(); 

            Material mat = render.sharedMaterial;
            render.material = new Material(mat);

            reactionTargetTrmList = new List<Transform>();
        }

        private void Update()
        {
            for(int i = 0; i < reactionTargetTrmList.Count; i++)
            {
                render.material.SetVector($"_{propertyName}{i + 1}", reactionTargetTrmList[i].position);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if((other.gameObject.layer & 1 << reactionLayer) != 0)
            {
                if(!(reactionTargetTrmList.Count >= maxReactionCount))
                {
                    reactionTargetTrmList.Add(other.transform);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            reactionTargetTrmList.Remove(other.transform);
        }
    }
}
