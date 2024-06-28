using TMPro;
using UnityEngine;

namespace OMG
{
    public class NameTag : MonoBehaviour
    {
        private TMP_Text nameTagText;

        private void Awake()
        {
            nameTagText = GetComponent<TMP_Text>();
        }

        public void SetNameTag(string name)
        {
            nameTagText.text = name;
        }
    }
}