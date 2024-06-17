using DG.Tweening;
using OMG.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.Minigames.SafetyZone
{
    public class SafetyTileBlock : MonoBehaviour
    {
        [SerializeField] Collider[] blocks = null;
        [SerializeField] Renderer[] renderers = null;
        private Material[] materials = null;

        public UnityEvent<bool> OnActiveChangedEvent;

        private void Start()
        {
            materials = new Material[renderers.Length];
            renderers.ForEach((i, index) => materials[index] = i.material);
        }

        public void SetActive(bool active)
        {
            ActiveDissolve(active);
            blocks.ForEach(i => i.enabled = active);
            // OnActiveChangedEvent?.Invoke(active);
        }

        private readonly string propertyID = "_Dissolve_Value";

        [SerializeField] private float defaultDissolveTime = 0.3f;
        
        public void ActiveDissolve(bool value)
        {
            if (value)
                ShowDissolve(defaultDissolveTime);
            else
                HideDissolve(defaultDissolveTime);
        }

        public void ShowDissolve(float time)
        {
            DODissolve(0f, 1.2f, time);
        }

        public void HideDissolve(float time)
        {
            DODissolve(1.2f, 0f, time);
        }

        public void DODissolve(float start, float end, float time)
        {
            StartCoroutine(this.PostponeFrameCoroutine(() => {
                materials.ForEach(i => {
                    i.SetFloat(propertyID, start);
                    i.DOFloat(end, propertyID, time);
                });
            }));
        }
    }
}
