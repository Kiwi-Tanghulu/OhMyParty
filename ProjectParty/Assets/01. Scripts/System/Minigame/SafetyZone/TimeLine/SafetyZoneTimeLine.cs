using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafetyZoneTimeLine : MonoBehaviour
{
    [SerializeField] private GameObject comicHitEffect;

    public void DisableTimeLineObject()
    {
        gameObject.SetActive(false);
    }


}
