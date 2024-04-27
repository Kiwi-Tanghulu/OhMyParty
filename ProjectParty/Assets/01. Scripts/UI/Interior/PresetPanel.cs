using OMG.Datas;
using OMG.Interiors;
using UnityEngine;
using UnityEngine.UI;

namespace OMG.UI.Interiors
{
    public class PresetPanel : MonoBehaviour
    {
        [SerializeField] Button presetButtonPrefab;
        [SerializeField] InteriorPresetComponent presetComponent = null;

        public void Init()
        {
            for(int i = 0; i < DataManager.UserData.InteriorData.PresetCount; ++i)
            {
                int index = i;
                Button button = Instantiate(presetButtonPrefab, transform);
                button.onClick.AddListener(() => presetComponent.LoadPreset(index));
            }
        }

        public void ClearPreset()
        {
            presetComponent.ClearPreset();
        }
    }
}
