using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.EatingLand
{
    public class EatingLandTileVisual : MonoBehaviour
    {
        [SerializeField] private EatingLandTileDataSO colorData;

        private MeshRenderer meshRenderer;

        [SerializeField] private float colorChangeDuration;
        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }

        public void ChangeVisual(int index)
        {
            StartCoroutine(ChangeColor(index));
        }

        private IEnumerator ChangeColor(int index)
        {
            float currentTime = 0;
            while (true)
            {

                currentTime += Time.deltaTime;

                if (currentTime > colorChangeDuration)
                {
                    break;
                }
                yield return null;
            }
        }
    }
}
