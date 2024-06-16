using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        private void Start()
        {
            if (initOnStart)
                Init();
        }

        public virtual void Init()
        {
            GetChildUI();

            for (int i = 0; i < childUI.Count; i++)
            {
                childUI[i].Init();
            }
        }

        public virtual void Show()
        {
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
            childUI = new();

            foreach (Transform child in transform)
            {
                if (child.TryGetComponent<UIObject>(out UIObject ui))
                    childUI.Add(ui);
            }
        }
    }
}
