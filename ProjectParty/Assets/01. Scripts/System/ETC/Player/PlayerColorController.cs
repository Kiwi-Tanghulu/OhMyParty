using System.Collections.Generic;
using UnityEngine;

namespace OMG.ETC
{
    public class PlayerColorController : MonoBehaviour
    {
        [System.Serializable]
        public class ControlParams
        {
            public string ColorPropertyName = "_BaseColor";
            public List<Renderer> Renderers = new List<Renderer>();
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
                foreach(Renderer r in control.Renderers)
                    r.material.SetColor(control.ColorPropertyName, PlayerManager.Instance.GetPlayerColor(index));
            }
        }

        public void SetIndex(int index)
        {
            this.index = index;
            SetColor();
        }
    }
}
