using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_RememberTimestamp : MonoBehaviour
{
    private AudioSource _audioSource;
    private int _time;

    void Update()
    {
        _time = _audioSource.timeSamples;
    }

    void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.timeSamples = _time;
        _audioSource.Play();
        
        Debug.Log(_time);
    }
}
