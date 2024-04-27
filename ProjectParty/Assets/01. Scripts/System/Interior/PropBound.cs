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

        private void Awake()
        {
            boundTransform = transform.Find("Bound");
            boundRenderer = boundTransform.GetComponent<MeshRenderer>();

            visualTransform = transform.Find("Visual");
        }

        public void Init(InteriorPropSO propData, float gridSize)
        {
            Clear();

            visualTransform.localPosition = propData.Pivot;
            GameObject visual = Instantiate(propData.VisualPrefab, visualTransform);
            visual.transform.localPosition = Vector3.zero;

            SetState(false);
        }

        public void SetState(bool enableToPlace)
        {
            boundRenderer.material = boundMaterialOption.GetOption(enableToPlace);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }

        private void Clear()
        {
            Destroy(visualTransform.GetChild(0).gameObject);
        }
    }
}
