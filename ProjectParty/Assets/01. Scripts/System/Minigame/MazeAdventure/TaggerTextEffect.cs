using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaggerTextEffect : MonoBehaviour
{
    [SerializeField] private EmotionText emotionTextPrefab;
    [SerializeField] private Vector3 effectPos;
    [SerializeField] private Vector3 effectRotation;
    public void MakeTextEffect(char text)
    {
        EmotionText emotionText = Instantiate(emotionTextPrefab, transform.position + effectPos, Quaternion.Euler(effectRotation));
        emotionText.StartEffect(text);
    }
}
