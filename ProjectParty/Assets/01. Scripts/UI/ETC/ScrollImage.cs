using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ScrollImage : MonoBehaviour
{
    private RawImage rawImage;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float speed;

    private void Start()
    {
        rawImage = GetComponent<RawImage>();
    }

    private void Update()
    {
        rawImage.uvRect = new Rect(rawImage.uvRect.position + direction.normalized * speed * Time.deltaTime, rawImage.uvRect.size);
    }
}
