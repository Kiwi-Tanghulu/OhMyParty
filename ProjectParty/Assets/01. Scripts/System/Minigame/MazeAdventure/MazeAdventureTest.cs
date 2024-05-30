using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeAdventureTest : MonoBehaviour
{
    void Start()
    {
        float floatDistance = 0.5f;
        float floatDuration = 2f;

        Vector3 upPosition = transform.position + new Vector3(0, floatDistance, 0);

        transform.DOMove(upPosition, floatDuration)
                 .SetEase(Ease.InOutSine)
                 .SetLoops(-1, LoopType.Yoyo);
    }

}
