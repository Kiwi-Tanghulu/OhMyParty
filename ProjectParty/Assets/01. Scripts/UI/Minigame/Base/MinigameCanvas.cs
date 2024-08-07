using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI
{
    public class MinigameCanvas : MonoBehaviour
    {
        public void ChangeRenderTypea(float scale)
        {
            Canvas canvas = GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = 0.5f;
            foreach(Transform child in transform)
            {
                child.localScale *= scale;
            }
        }
    }
}