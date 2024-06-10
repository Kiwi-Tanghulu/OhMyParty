using AllIn1VfxToolkit;
using OMG.Minigames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    public class DistanceAlphaController : MonoBehaviour
    {
        [SerializeField] private Material distanceAlphaMat;
        private readonly string propertyName = "ReactionPos";

        private PlayableMinigame minigame;

        private bool isInit = false;

        public void Init()
        {
            minigame = MinigameManager.Instance.CurrentMinigame as PlayableMinigame;

            if (minigame == null)
                enabled = false;

            isInit = true;
        }

        private void Update()
        {
            if (!isInit)
                return;

            int cnt = 0;

            foreach(var pair in minigame.PlayerDictionary)
            {
                cnt++;
                distanceAlphaMat.SetVector($"_{propertyName}{cnt}", pair.Value.transform.position);
            }
        }
    }
}