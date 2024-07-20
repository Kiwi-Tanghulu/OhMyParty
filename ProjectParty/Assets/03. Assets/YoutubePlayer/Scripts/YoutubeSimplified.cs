﻿using System.Collections;
using System.Collections.Generic;
using LightShaft.Scripts;
using UnityEngine;
using UnityEngine.Video;

public class YoutubeSimplified : MonoBehaviour
{
    private YoutubePlayer player;

    public string url;
    public bool autoPlay = true;
    public bool fullscreen = true;
    [SerializeField] private VideoPlayer videoPlayer;

    private void Awake()
    {
        player = GetComponentInChildren<YoutubePlayer>();
        player.videoPlayer = videoPlayer;
    }

    //private void Start()
    //{
    //    player.dontForceFullscreen = true;
    //}

    public void Play()
    {
        if (fullscreen)
        {
            videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
            videoPlayer.aspectRatio = VideoAspectRatio.FitInside;
            videoPlayer.targetCamera = player.mainCamera;
            
        }
        player.autoPlayOnStart = autoPlay;
        player.videoQuality = YoutubePlayer.YoutubeVideoQuality.STANDARD;

        player.Play(url);
    }
}
