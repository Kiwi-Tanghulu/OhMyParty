using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OMG
{
    public class Dissolve : MonoBehaviour
    {
        private Material mat;
        private readonly string propertyID = "_Dissolve_Value";

        [SerializeField] private float defaultDissolveTime = 0.3f;

        private void Start()
        {
            Renderer render = GetComponent<Renderer>();
            Material sharedMat = render.sharedMaterial;
            mat = new Material(sharedMat);
            render.material = mat;
        }
        
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
            if (mat == null)
                return;

            mat.SetFloat(propertyID, start);
            mat.DOFloat(end, propertyID, time);
        }
    }
}