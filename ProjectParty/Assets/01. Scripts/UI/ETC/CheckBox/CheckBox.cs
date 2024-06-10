using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OMG.UI
{
    public class CheckBox : MonoBehaviour
    {
        public UnityEvent OnCheckedEvent;

        public virtual void SetCheck(bool value)
        {
            if(value)
                OnCheckedEvent?.Invoke();
        }
    }
}