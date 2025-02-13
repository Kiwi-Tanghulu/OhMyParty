using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace OMG.UI
{
    public class UIObject : MonoBehaviour
    {
        private RectTransform rect;
        public RectTransform Rect
        {
            get
            {
                if(rect == null)
                    rect = GetComponent<RectTransform>();

                return rect;
            }
        }

        private List<UIObject> childUI;

        [SerializeField] private bool initOnStart;
        private bool isInit;
        public bool IsInit => isInit;

        protected virtual void Start()
        {
            if (initOnStart)
                Init();
        }

        public virtual void Init()
        {
            childUI = new();

            GetChildUI();

            for (int i = 0; i < childUI.Count; i++)
            {
                childUI[i].Init();
            }

            isInit = true;
        }

        public virtual void Show()
        {
            if (IsInit == false)
                Init();

            gameObject.SetActive(true);

            for (int i = 0; i < childUI.Count; i++)
            {
                childUI[i].Show();
            }
        }

        public virtual void Hide() 
        {
            gameObject.SetActive(false);

            for (int i = 0; i < childUI.Count; i++)
            {
                childUI[i].Hide();
            }
        }

        private void GetChildUI()
        {
            foreach (Transform child in transform)
            {
                if (child.TryGetComponent<UIObject>(out UIObject ui))
                    childUI.Add(ui);
            }
        }
    }
}
