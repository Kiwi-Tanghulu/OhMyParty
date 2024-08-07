using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OMG.Minigames.EatingLand
{
    public class EatingLandTileVisual : MonoBehaviour
    {
        [SerializeField] private EatingLandTileDataSO colorData;
        [SerializeField] private float colorChangeDuration;

        private Material material;

        private void Awake()
        {
            material = GetComponent<Renderer>().material;
        }

        public void ChangeVisual(int index)
        {
            StopAllCoroutines();
            StartCoroutine(ChangeColor(index));
        }

        private IEnumerator ChangeColor(int index)
        {
            float currentTime = 0;

            Color startColor = colorData.EffectColors[index];
            Color endColor = colorData.TileColors[index];

            material.color = startColor;

            while (true)
            {

                material.color = Color.Lerp(startColor, endColor, currentTime / colorChangeDuration);

                currentTime += Time.deltaTime;

                if (currentTime > colorChangeDuration)
                {
                    material.color = endColor;
                    break;
                }
                yield return null;
            }
        }
    }
}
