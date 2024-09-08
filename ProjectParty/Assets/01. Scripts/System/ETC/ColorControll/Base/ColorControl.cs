using System;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.ColorControlling
{
    [Serializable]
    public abstract class ColorControl<T> : ColorControl
    {
        [SerializeField] protected List<T> drawers = new List<T>();

        public override void SetColor(Color color)
        {
            foreach(T drawer in drawers)
                ChangeColor(drawer, color);
        }

        protected abstract void ChangeColor(T drawer, Color color);
    }

    public abstract class ColorControl 
    { 
        public abstract void SetColor(Color color);
    }
}