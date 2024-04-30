using UnityEngine;
using OMG.UI.Interiors;

namespace OMG.Interiors
{
    public class InteriorProp : MonoBehaviour
    {
        [SerializeField] InteriorPropSO propData = null;
        public InteriorPropSO PropData => propData;

        public PlacementData PlacementData { get; private set; }

        private Camera mainCam = null;
        private PropModifyPanel modifyPanel = null;

        private void Awake()
        {
            mainCam = Camera.main;
            modifyPanel = DEFINE.MainCanvas.Find("PropModifyPanel").GetComponent<PropModifyPanel>();
        }

        public void Init(PlacementData data)
        {
            PlacementData = data;
        }

        public void SetPropData(InteriorPropSO propData)
        {
            this.propData = propData;
        }

        public void SetModifyPanel()
        {
            if(modifyPanel.Active)
                return;

            modifyPanel.Init(this);
            modifyPanel.Display(true);
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(propData != null)
                propData.DrawGizmos(transform.position, 1f);
        }
        #endif
    }
}
