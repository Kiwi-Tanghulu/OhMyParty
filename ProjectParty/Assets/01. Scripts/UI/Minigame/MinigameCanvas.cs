using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI
{
    public class MinigameCanvas : MonoBehaviour
    {
        private void Awake()
        {
            Canvas canvas = GetComponent<Canvas>();
            canvas.worldCamera = Camera.main;
            canvas.planeDistance = 0.5f;
        }
    }
}