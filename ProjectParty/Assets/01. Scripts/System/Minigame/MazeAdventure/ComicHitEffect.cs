using TMPro;
using UnityEngine;

namespace OMG
{
    public class ComicHitEffect : MonoBehaviour
    {
        private TextMeshPro tmp;

        private void Awake()
        {
            tmp = GetComponent<TextMeshPro>();

            GetComponent<OMG.AnimationEvent>().OnEndEvent += AnimationEvent_OnEndEvent;
        }

        private void AnimationEvent_OnEndEvent()
        {
            Destroy(gameObject);
        }

        public void SetText(string text)
        {
            tmp.text = text;
        }
    }
}