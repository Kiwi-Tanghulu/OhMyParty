using OMG;
using System.Collections.Generic;
using UnityEngine;

public class TJunsung : MonoBehaviour
{
    [SerializeField] private List<string> urls;
    [SerializeField] private OMGVideoPlayer player;
    
    int cnt = 0;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            player.Play(urls[cnt % urls.Count]);
            cnt++;
        }
    }
}