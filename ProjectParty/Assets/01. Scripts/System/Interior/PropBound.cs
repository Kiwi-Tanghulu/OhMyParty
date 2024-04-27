using OMG.Utility;
using UnityEngine;

namespace OMG.Interiors
{
    public class PropBound : MonoBehaviour
    {
        [SerializeField] OptOption<Material> boundMaterialOption;
        [SerializeField] OptOption<Material> meshMaterialOption;

        private Transform boundTransform = null;
        private Transform visualTransform = null;

        private MeshRenderer boundRenderer = null;
        private MeshRenderer visualRenderer = null;
        private MeshFilter visualMeshFilter = null;

        private void Awake()
        {
            boundTransform = transform.Find("Bound");
            boundRenderer = boundTransform.GetComponent<MeshRenderer>();

            visualTransform = transform.Find("Visual");
            visualRenderer = visualTransform.GetComponent<MeshRenderer>();
            visualMeshFilter = visualTransform.GetComponent<MeshFilter>();
        }

        public void Init(InteriorPropSO propData, float gridSize)
        {
            visualTransform.localPosition = propData.Pivot;
            visualMeshFilter.mesh = propData.PropMesh;
            SetState(false);
        }

        public void SetState(bool enableToPlace)
        {
            boundRenderer.material = boundMaterialOption.GetOption(enableToPlace);
            visualRenderer.material = meshMaterialOption.GetOption(enableToPlace);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
