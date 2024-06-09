using System.Collections.Generic;
using OMG.Tweens;
using OMG.Extensions;
using TMPro;
using UnityEngine;

namespace OMG.UI.Minigames.SafetyZones
{
    public class TimerPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text timeText = null;

        [Space(15f)]
        [SerializeField] TweenSO idleTween = null;
        [SerializeField] List<TweenSO> explosionTweens = null;
        [SerializeField] Transform tweenBody = null;

        private void Awake()
        {
            idleTween = idleTween.CreateInstance(tweenBody);
            for(int i = 0; i < explosionTweens.Count; ++i)
                explosionTweens[i] = explosionTweens[i].CreateInstance(tweenBody);
        }

        public void SetText(float time)
        {
            time = Mathf.Max(0f, time);
            timeText.text = Mathf.FloorToInt(time).ToString();
        }

        public void HandleReroll()
        {
            idleTween.ForceKillTween();
            explosionTweens.ForEach(LoopTween);
        }

        public void HandleDecision()
        {
            explosionTweens.ForEach(i => i.ForceKillTween());

            timeText.text = "";
            timeText.gameObject.SetActive(false);
        }

        public void HandleReset()
        {
            timeText.gameObject.SetActive(true);
            LoopTween(idleTween);
        }

        private void LoopTween(TweenSO tween)
        {
            tween.PlayTween(() => LoopTween(tween));
        }
    }
}
