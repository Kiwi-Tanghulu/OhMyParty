using OMG.Utility;
using UnityEngine;

namespace OMG.Interiors
{
    public class InteriorVisualComponent : MonoBehaviour
    {
        [SerializeField] Transform bound = null;
        [SerializeField] GameObject gridObject = null;

        [Space(15f)]
        [SerializeField] OptOption<Material> boundMaterialOption;
        [SerializeField] OptOption<Material> meshMaterialOption;

        private Transform boundTransform = null;
        private Transform visualTransform = null;

        private MeshRenderer boundRenderer = null;
        private MeshRenderer visualRenderer = null;
        private MeshFilter visualMeshFilter = null;

        private float gridSize = 0f;

        private void Awake()
        {
            boundTransform = bound.Find("Bound");
            boundRenderer = boundTransform.GetComponent<MeshRenderer>();

            visualTransform = bound.Find("Visual");
            visualRenderer = visualTransform.GetComponent<MeshRenderer>();
            visualMeshFilter = visualTransform.GetComponent<MeshFilter>();
        }

        public void Init(float gridSize) 
        {
            this.gridSize = gridSize;
            Display(false);
        }

        public void Display(bool active)
        {
            gridObject.SetActive(active);
            bound.gameObject.SetActive(active);
        }

        public void SetPropBound(InteriorPropSO propData)
        {
            visualTransform.localPosition = propData.Pivot * gridSize;
            visualMeshFilter.mesh = propData.PropMesh;
            boundTransform.localScale = new Vector3(propData.PropSize.x * gridSize, 1f, propData.PropSize.z * gridSize);
            UpdateBound(Vector3.zero, false);
        }

        public void UpdateBound(Vector3 position, bool enableToPlace)
        {
            boundRenderer.material = boundMaterialOption.GetOption(enableToPlace);
            visualRenderer.material = meshMaterialOption.GetOption(enableToPlace);
            
            bound.position = position;
        }
    }
}
