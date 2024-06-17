using DG.Tweening;
using OMG.Extensions;
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
            // Material sharedMat = render.sharedMaterial;
            // mat = new Material(sharedMat);
            // render.material = mat;
            mat = render.material;
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
            Debug.Log("Dissolve Begin");
            StartCoroutine(this.PostponeFrameCoroutine(() => {
                mat.SetFloat(propertyID, start);
                mat.DOFloat(end, propertyID, time);
            }));
            Debug.Log("Dissolve End");
            // StopAllCoroutines();
            // StartCoroutine(DissolveRoutine(start, end, time));
        }

        private IEnumerator DissolveRoutine(float start, float end, float time)
        {
            mat.SetFloat(propertyID, start);
            float timer = 0f;

            while(timer < time)
            {
                mat.SetFloat(propertyID, Mathf.Lerp(start, end, timer / time));
                Debug.Log(timer / time);
                
                timer += Time.deltaTime;
                yield return null;
            }

            mat.SetFloat(propertyID, end);
        }
    }
}