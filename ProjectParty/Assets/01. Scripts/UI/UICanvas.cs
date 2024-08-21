using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI
{
    public class UICanvas : UIObject
    {
        private Dictionary<Type, UIPanel> panels;

        protected override void Start()
        {
            Init();
        }

        public override void Init()
        {
            base.Init();

            panels = new Dictionary<Type, UIPanel>();
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent<UIPanel>(out UIPanel panel))
                {
                    panels.Add(panel.GetType(), panel);
                    panel.Init();
                }
            }
        }

        public T GetPanel<T>() where T : UIPanel
        {
            return panels[typeof(T)] as T;
        }

    }
}