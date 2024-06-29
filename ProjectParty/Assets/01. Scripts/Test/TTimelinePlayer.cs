using System;
using System.Collections;
using System.Collections.Generic;
using OMG.Inputs;
using UnityEngine;
using UnityEngine.Playables;

public class TTimelinePlayer : MonoBehaviour
{
    [SerializeField] private UIInputSO _inputSO;
    private PlayableDirector _director;

    private void Awake() 
    {
        _director = GetComponent<PlayableDirector>();
        _inputSO.OnSpaceEvent += PlayTimeline;
        InputManager.ChangeInputMap(InputMapType.UI);
    }

    private void PlayTimeline()
    {
        Debug.Log("Play");
        _director.Play();
    }
}
