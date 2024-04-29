using OMG.Extensions;
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

        private Transform boundTransform = null;
        private Transform visualTransform = null;

        private MeshRenderer boundRenderer = null;

        private Vector3 offset = Vector3.zero;
        private Vector3 pivot = Vector3.zero;

        private float gridSize = 0f;

        private void Awake()
        {
            boundTransform = bound.Find("Bound");
            boundRenderer = boundTransform.GetComponent<MeshRenderer>();

            visualTransform = bound.Find("Visual");
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
            ClearVisual();
            offset = propData.Center.PlaneVector();

            GameObject visual = Instantiate(propData.VisualPrefab, visualTransform);
            visual.transform.localPosition -= offset;

            boundTransform.localScale = new Vector3(propData.PropSize.x * gridSize, propData.PropSize.z * gridSize, 1f);

            UpdateBound(Vector3.zero, false);
        }

        public void SetRotate(int amount)
        {
            bound.position = pivot + offset;
            bound.RotateAround(pivot, Vector3.up, amount * 90f);
            offset = bound.position - pivot;
        }

        public void UpdateBound(Vector3 position, bool enableToPlace)
        {
            boundRenderer.material = boundMaterialOption.GetOption(enableToPlace);
            bound.position = position + offset;
            pivot = position;
        }

        private void ClearVisual()
        {
            foreach(Transform child in visualTransform)
                Destroy(child?.gameObject);
        }
    }
}
