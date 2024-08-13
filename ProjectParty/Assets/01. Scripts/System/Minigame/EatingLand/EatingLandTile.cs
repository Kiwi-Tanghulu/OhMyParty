using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace OMG.Minigames.EatingLand
{
    public class EatingLandTile : MonoBehaviour
    {
        private int tileID;
        private int currentIndex = -1;

        private EatingLandTileVisual eatingLandTileVisual;
        public event Action<int, int, int> OnUpdateTileEvent;

        private void Awake()
        {
            eatingLandTileVisual = GetComponent<EatingLandTileVisual>();
        }
        public void Init(int id)
        {
            tileID = id;
        }

        private void Start()
        {
            currentIndex = -1;
        }

        public void OnPlayerEnter(int nextIndex)
        {
            if (currentIndex == nextIndex)
            {
                return;
            }
            OnUpdateTileEvent?.Invoke(tileID, currentIndex ,nextIndex);
        }

        public void UpdateTile(int nextIndex)
        {
            eatingLandTileVisual.ChangeVisual(nextIndex);
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
