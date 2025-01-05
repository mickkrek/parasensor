using UnityEngine;
using Pixelplacement;

public class Music_FadeIn : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _initialVolume;
    [SerializeField] private float _delay = 0f;
    [SerializeField] private float _fadeDuration = 4f;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _initialVolume = _audioSource.volume;
        _audioSource.volume = 0f;
        _audioSource.PlayDelayed(_delay);
        Tween.Volume(_audioSource, _initialVolume, _fadeDuration, _delay, Tween.EaseLinear);
    }
}
