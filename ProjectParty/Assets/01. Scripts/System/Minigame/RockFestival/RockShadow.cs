using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace OMG.Minigames.RockFestival
{
    public class RockShadow : MonoBehaviour
    {
        [SerializeField] AnimationCurve scaleCurve = null;
        [SerializeField] float maxDistance = 10f;
        [SerializeField] float depth = 12f;
        [SerializeField] LayerMask groundLayer = 0;
        private DecalProjector decalRenderer = null;

        private bool render = true;

        private void Awake()
        {
            decalRenderer = transform.Find("DropShadow").GetComponent<DecalProjector>();
        }

        private void Update()
        {
            if(render == false)
                return;

            Vector3 size = Vector3.zero;

            float distance = GetDistance();
            if(distance == -1)
            {
                size = Vector2.zero;
            }
            else
            {
                distance = Mathf.Clamp(distance, 0f, maxDistance);
                float ratio = (maxDistance - distance) / maxDistance;
                float factor = Mathf.Lerp(0f, 1f, scaleCurve.Evaluate(ratio));
                size = Vector2.one * factor;
            }

            size.z = depth;
            decalRenderer.size = size;
        }

        public void Display(bool active)
        {
            render = active;
        }

        private float GetDistance()
        {
            Ray ray = new Ray(transform.position, Vector3.down);
            if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, groundLayer))
                return hit.distance;

            return -1;
        }
    }
}