using OMG.UI;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

namespace OMG.Lobbies
{
    public class MinigameSpotTV : MonoBehaviour
    {
        private Canvas screen;

        private CountText roundCountText;

        private void Awake()
        {
            screen = transform.Find("Screen").GetComponent<Canvas>();
            roundCountText = screen.transform.Find("MinigameCountText").GetComponent<CountText>();

            SetScreenActive(false);
        }

        public void SetScreenActive(bool value)
        {
            screen.gameObject.SetActive(value);
        }

        public void SetRoundCountText(int round)
        {
            roundCountText.SetCount(round);
        }

        public void SetRoundCountValue(int cycleCount)
        {
            roundCountText.SetCountValue(0, cycleCount);
        }
    }
}