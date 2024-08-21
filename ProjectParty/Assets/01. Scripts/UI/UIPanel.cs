using OMG.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI
{
    public class UIPanel : UIObject
    {
        public event Action OnHide;

        protected override void Start()
        {
            base.Start();

            gameObject.SetActive(false);
        }

        public override void Show()
        {
            base.Show();

            UIManager.Instance.ShowPanel(this);
        }

        public override void Hide()
        {
            base.Hide();

            OnHide?.Invoke();
        }

        public virtual void OnlyShow()
        {
            gameObject.SetActive(true);
        }
    }
}
