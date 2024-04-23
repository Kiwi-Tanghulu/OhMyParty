using OMG.Extensions;
using OMG.Tweens;
using UnityEngine;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTileBlock : MonoBehaviour
    {
        [SerializeField] TweenOptOption tweenOption = null;
        [SerializeField] TweenOptOption immediatelyTweenOption = null;

        [SerializeField] Collider[] blocks = null;

        private void Awake()
        {
            tweenOption.Init(transform);
            immediatelyTweenOption.Init(transform);
        }

        public void SetActive(bool active, bool immediately = false)
        {
            blocks.ForEach(i => i.isTrigger = !active);

            // gameObject.SetActive(active);
            ClearTween();

            if(immediately)
                immediatelyTweenOption.GetOption(active).PlayTween();
            else
               tweenOption.GetOption(active).PlayTween();
        }

        private void ClearTween()
        {
            tweenOption.GetOption(true).ForceKillTween();
            tweenOption.GetOption(false).ForceKillTween();
        }
    }
}
