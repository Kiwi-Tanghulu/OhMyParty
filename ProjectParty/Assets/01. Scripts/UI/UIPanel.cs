using OMG.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI
{
    public class UIPanel : UIObject
    {
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

        public virtual void OnlyShow()
        {
            base.Show();
        }
    }
}
