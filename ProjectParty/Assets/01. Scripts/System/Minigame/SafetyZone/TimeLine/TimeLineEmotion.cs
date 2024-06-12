using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.SafetyZone
{
    public class TimeLineEmotion : MonoBehaviour
    {
        private EmotionText emotionText;

        private void Awake()
        {
            emotionText = GetComponent<EmotionText>();
        }

        private void Start()
        {
            emotionText.gameObject.SetActive(false);
        }

        public void StartEffect()
        {
            emotionText.gameObject.SetActive(true);
            emotionText.StartEffect('?');
        }
    }
}
