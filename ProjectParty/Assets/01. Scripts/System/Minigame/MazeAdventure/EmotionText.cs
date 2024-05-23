using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class EmotionText : MonoBehaviour
{
    private TextMeshPro textMeshPro;
    private Sequence effectSequence;
    private Vector3 originSize;
    private Color originColor;
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }
    private void Start()
    {
        effectSequence = DOTween.Sequence();
        originSize = transform.localScale;
        originColor = textMeshPro.color;
    }
    public void StartEffect(char text)
    {
        textMeshPro.text = text.ToString();
        ScaleEffect();
    }
    private void ScaleEffect()
    {
        //if(effectSequence == null)
        //{
            effectSequence.Append(transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutExpo))
                .AppendInterval(1f).Append(textMeshPro.DOFade(0, 0.3f)).OnComplete(ResetSetting).
                Insert(0f, transform.DORotate(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Random.Range(-10f, 10f)), 0.25f));
        //}
        effectSequence.Play();
    }
    private void ResetSetting()
    {
        transform.localScale = originSize;
        textMeshPro.color = originColor;
    }
    
}
