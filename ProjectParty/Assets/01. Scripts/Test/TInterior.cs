using OMG.Input;
using OMG.Interiors;
using UnityEngine;

namespace OMG.Test
{
    public class TInterior : MonoBehaviour
    {
        [SerializeField] InteriorPropSO propData = null;
        private InteriorSystem system = null;
        private InteriorPresetComponent presetComponent = null;

        private void Awake()
        {
            system = GetComponent<InteriorSystem>();
            presetComponent = GetComponent<InteriorPresetComponent>();
        }

        private void Start()
        {
            InputManager.ChangeInputMap(InputMapType.Interior);
            system.SetPropData(propData.PropID);
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha1))
                presetComponent.LoadPreset(0);
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha2))
                presetComponent.LoadPreset(1);
            if (UnityEngine.Input.GetKeyDown(KeyCode.Alpha3))
                presetComponent.LoadPreset(2);
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                presetComponent.CreatePreset();
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(system == null)
                return;

            int count = 100;
            Gizmos.color = Color.black;
            {
                Vector3 startPosition = new Vector3(-100, 0, 0);
                Vector3 endPosition = new Vector3(100, 0, 0);
                for (int i = -count / 2; i < count / 2; ++ i)
                {
                    endPosition.z = i * system.GridSize;
                    startPosition.z = i * system.GridSize;
                    Gizmos.DrawLine(startPosition, endPosition);
                }
            }
            {
                Vector3 startPosition = new Vector3(0, 0, -100);
                Vector3 endPosition = new Vector3(0, 0, 100);
                for (int i = -count / 2; i < count / 2; ++ i)
                {
                    endPosition.x = i * system.GridSize;
                    startPosition.x = i * system.GridSize;
                    Gizmos.DrawLine(startPosition, endPosition);
                }
            }
        }
        #endif
    }
}
