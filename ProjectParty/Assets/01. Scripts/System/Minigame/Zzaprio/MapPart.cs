using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.Minigames
{
    public class MapPart : MonoBehaviour
    {
        [SerializeField] private Transform startTrm;
        [SerializeField] private Transform endTrm;

        public Transform StartTrm => startTrm;
        public Transform EndTrm => endTrm;

        public void SetPosition(MapPart frontMapPart)
        {
            Vector3 pos = Vector3.zero;

            if(frontMapPart != null)
            {
                float leftLenght = transform.position.x - startTrm.position.x;
                pos = frontMapPart.EndTrm.position + Vector3.right * leftLenght;
            }

            transform.position = pos;
        }
    }
}