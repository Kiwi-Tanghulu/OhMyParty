using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG.UI
{
    public class CheckBox : MonoBehaviour
    {
        [SerializeField] private GameObject checkObject;

        public void SetCheck(bool value)
        {
            checkObject.SetActive(value);
        }
    }
}