using TMPro;
using UnityEngine;

namespace OMG
{
    public class ComicHitEffect : AnimationEffect
    {
        private TextMeshPro tmp;

        public override void Awake()
        {
            base.Awake();

            tmp = GetComponent<TextMeshPro>();
        }

        public void SetText(string text)
        {
            tmp.text = text;
        }
    }
}