using OMG.Interiors;
using OMG.Utility;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI.Interiors
{
    public class InteriorPanel : MonoBehaviour
    {
        [SerializeField] OptOption<UnityEvent> onDisplayEvent = null;

        private PresetPanel presetPanel = null;

        private void Awake()
        {
            presetPanel = transform.Find("PresetPanel").GetComponent<PresetPanel>();
        }

        private void Start()
        {
            presetPanel.Init();
            gameObject.SetActive(false);
        }

        public void Display(bool active)
        {
            onDisplayEvent.GetOption(active).Invoke();
        }
    }
}
