using System.Collections.Generic;
using UnityEngine;

namespace OMG.ColorControlling
{
    public abstract class ColorController : MonoBehaviour
    {
        [SerializeField] bool initOnAwake = true;

        [Space(15f)]
        [SerializeField] List<RendererControl> renderers = new List<RendererControl>();
        [SerializeField] List<GraphicControl> graphics = new List<GraphicControl>(); 

        private void Awake()
        {
            if (initOnAwake)
                SetColor();
        }

        public void SetColor()
        {
            Color color = GetColor();

            foreach (ColorControl control in renderers)
                control.SetColor(color);
            foreach (ColorControl control in graphics)
                control.SetColor(color);
        }

        protected abstract Color GetColor();
    }
}