using OMG.Tweens;
using TMPro;
using UnityEngine;

namespace OMG.UI.Minigames.SafetyZones
{
    public class TimerPanel : MonoBehaviour
    {
        [SerializeField] TMP_Text timeText = null;

        [Space(15f)]
        [SerializeField] TweenSO idleTween = null;
        [SerializeField] TweenSO explosionTween = null;
        [SerializeField] Transform tweenBody = null;

        private void Awake()
        {
            idleTween = idleTween.CreateInstance(tweenBody);
            explosionTween = explosionTween.CreateInstance(tweenBody);
        }

        public void SetText(float time)
        {
            time = Mathf.Max(0f, time);
            timeText.text = Mathf.FloorToInt(time).ToString();
        }

        public void HandleReroll()
        {
            idleTween.ForceKillTween();
            LoopTween(explosionTween);
        }

        public void HandleDecision()
        {
            explosionTween.ForceKillTween();

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
