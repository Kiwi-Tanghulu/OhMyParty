using System;
using UnityEngine;

namespace OMG.ColorControlling
{
    [Serializable]
    public class RendererControl : ColorControl<Renderer>
    {
        [SerializeField] string colorPropertyName = "_BaseColor";

        protected override void ChangeColor(Renderer drawer, Color color)
        {
            drawer.material.SetColor(colorPropertyName, color);
        }
    }
}