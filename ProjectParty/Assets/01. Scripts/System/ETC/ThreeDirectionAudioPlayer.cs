using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ThreeDirectionAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(Vector3 position, float distance)
    {
        transform.position = position;
        audioSource.maxDistance = distance;
        audioSource.Play();
    }

    public void Play(AudioClip clip, Vector3 position, float distance)
    {   
        audioSource.clip = clip;
        Play(position, distance);
    }

    public void PlayOneShot(AudioClip clip, Vector3 position, float distance)
    {
        transform.position = position;
        audioSource.maxDistance = distance;
        audioSource.PlayOneShot(clip);
    }
}
