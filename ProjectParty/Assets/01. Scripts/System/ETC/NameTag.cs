using TMPro;
using UnityEngine;

namespace OMG
{
    [RequireComponent(typeof(TextMeshPro))]
    public class NameTag : MonoBehaviour
    {
        private TextMeshPro nameTagText;

        private void Awake()
        {
            nameTagText = GetComponent<TextMeshPro>();
        }

        public void SetNameTag(string name)
        {
            nameTagText.text = name;
        }
    }
}