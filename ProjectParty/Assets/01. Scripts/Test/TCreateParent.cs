using OMG.Interiors;
using UnityEngine;

namespace OMG.Test
{
    public class TCreateParent : MonoBehaviour
    {
        private void OnValidate()
        {
            if(transform.parent != null)
                return;

            GameObject visualParent = new GameObject($"{gameObject.name}_Visual");
            transform.parent = visualParent.transform;

            GameObject rootParent = new GameObject(gameObject.name);
            visualParent.transform.parent = rootParent.transform;

            rootParent.AddComponent<InteriorProp>();
            rootParent.AddComponent<BoxCollider>();
        }   
    }
}
