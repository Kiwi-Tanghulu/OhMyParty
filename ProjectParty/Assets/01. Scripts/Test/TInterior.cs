using OMG.Input;
using OMG.Interiors;
using UnityEngine;

namespace OMG.Test
{
    public class TInterior : MonoBehaviour
    {
        private InteriorSystem system = null;

        private void Awake()
        {
            system = GetComponent<InteriorSystem>();
        }

        private void Start()
        {
            InputManager.ChangeInputMap(InputMapType.Interior);
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
