using OMG;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SafetyZoneTimeLine : MonoBehaviour
{
    [SerializeField] private Dissolve[] wall;

    public void DissolveWall()
    {
        foreach (var wall in wall) 
        {
            wall.ShowDissolve(0.3f);
        }
    }
    public void DisableTimeLineObject()
    {
        gameObject.SetActive(false);
    }


}
