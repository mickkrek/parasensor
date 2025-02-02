using Pixelplacement;
using UnityEngine;

public class MainMenu_Music : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _initialVolume;

    public void Play()
    {
        _audioSource = GetComponent<AudioSource>();
        _initialVolume = _audioSource.volume;
        _audioSource.volume = 0f;
        _audioSource.PlayDelayed(2f);
        Tween.Volume(_audioSource, _initialVolume, 1f, 2f, Tween.EaseLinear);
    }

    public void EndMusic()
    {
        Tween.Volume(_audioSource, 0f, 2f, 0f, Tween.EaseLinear);
        Tween.Pitch(_audioSource, 0.2f, 0.5f, 0f, Tween.EaseLinear);
    }
}
