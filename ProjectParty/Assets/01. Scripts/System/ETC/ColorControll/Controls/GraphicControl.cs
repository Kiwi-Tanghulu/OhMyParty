using System;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.ColorControlling
{
    [Serializable]
    public class GraphicControl : ColorControl<Graphic>
    {
        protected override void ChangeColor(Graphic drawer, Color color)
        {
            drawer.color = color;
        }
    }
}