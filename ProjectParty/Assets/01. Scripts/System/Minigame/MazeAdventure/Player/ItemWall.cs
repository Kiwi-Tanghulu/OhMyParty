using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.MazeAdventure
{
    public class ItemWall : MonoBehaviour
    {
        [SerializeField] private float keepTime;

        public void StartTimer()
        {
            StartCoroutine(DestoryTimer());
        }
        private IEnumerator DestoryTimer()
        {
            yield return new WaitForSeconds(keepTime);
            Destroy(gameObject);
        }
    }
}
