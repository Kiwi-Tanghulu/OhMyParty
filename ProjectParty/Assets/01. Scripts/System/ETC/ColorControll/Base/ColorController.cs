using System.Collections.Generic;
using UnityEngine;

namespace OMG.ColorControlling
{
    public abstract class ColorController : MonoBehaviour
    {
        [SerializeField] bool initOnAwake = true;

        [Space(15f)]
        [SerializeField] List<RendererControl> rendererControls = new List<RendererControl>();
        [SerializeField] GraphicControl graphicControl = new GraphicControl();

        private void Awake()
        {
            if (initOnAwake)
                SetColor();
        }

        public void SetColor()
        {
            Color color = GetColor();

            foreach (ColorControl control in rendererControls)
                control.SetColor(color);
            graphicControl.SetColor(color);
        }

        protected abstract Color GetColor();
    }
}