using OMG.Minigames.RunAway;
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

        [SerializeField] private PlayerItemBox itemBoxPrefab;
        [SerializeField] private Transform itemSpawnPoints; 

        public void Init(MapPart frontMapPart, bool createItem)
        {
            SetPosition(frontMapPart);

            //if(createItem)
            //{
            //    foreach (Transform point in itemSpawnPoints)
            //    {
            //        PlayerItemBox itemBox = Instantiate(itemBoxPrefab, point.position, point.rotation);
            //        itemBox.NetworkObject.Spawn();
            //        itemBox.NetworkObject.TrySetParent(transform);
            //    }
            //}
        }

        private void SetPosition(MapPart frontMapPart)
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