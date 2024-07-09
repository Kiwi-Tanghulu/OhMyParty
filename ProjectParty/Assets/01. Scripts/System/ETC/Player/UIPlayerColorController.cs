using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.ETC
{
    public class UIPlayerColorController : MonoBehaviour
    {
        [System.Serializable]
        public class ControlParams
        {
            public string ColorPropertyName = "_BaseColor";
            public List<Graphic> Graphics = new List<Graphic>();
        }

        [SerializeField] List<ControlParams> controls = new List<ControlParams>();
        [SerializeField] int index = 0;

        [Space(15f)]
        [SerializeField] bool initOnAwake = true;

        private void Awake()
        {
            if(initOnAwake)
                SetColor();
        }

        public void SetColor()
        {
            foreach(ControlParams control in controls)
            {
                foreach(Graphic r in control.Graphics)
                {
                    Color color = PlayerManager.Instance.GetPlayerColor(index);
                    r.color = color;
                }
            }
        }

        public void SetIndex(int index)
        {
            this.index = index;
            SetColor();
        }
    }
}
