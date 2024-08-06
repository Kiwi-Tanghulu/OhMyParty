using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.EatingLand
{
    public class EatingLandTile : MonoBehaviour
    {
        private int currentIndex = -1;

        private void Start()
        {
            currentIndex = -1;
        }
        public void OnPlayerEnter(int nextIndex)
        {
            if(currentIndex == nextIndex)
            {
                return;
            }
            currentIndex = nextIndex;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnPlayerEnter(other.GetComponent<EatingLandPlayerController>().PlayerIndex);
            }
        }
    }
}
