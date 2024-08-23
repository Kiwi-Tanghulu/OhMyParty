using OMG.Attributes;
using OMG.UI.Minigames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;


namespace OMG.Minigames.EatingLand
{
    public class EatingLandPlayerSlot : DeathmatchPlayerSlot
    {
        [SerializeField] TMP_Text scoreText = null;

        private int score = 0;
        protected override void Awake()
        {
            base.Awake();
            Reset();
        }

        public void Reset()
        {
            scoreText.text = "-";
            score = 0;
        }

        public void SetScoreText(int score)
        {
            scoreText.text = score.ToString();
        }
    }

}
