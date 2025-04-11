using Pixelplacement;
using UnityEngine;

public class AudioZone : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioNode[] _nodes;
    [SerializeField]] private float _transitionPos;

    public struct AudioNode
    {
        public Transform position;
        [Range(0,1)]public float volume;
    }

    void Update()
    {
        for (int i = 0; i < _nodes.Length; i++)
        {
            Vector3 interpolatedPos = Vector3.Lerp(_nodes[i].position)
            _audioSource.transform.position = _nodes[i].position
            _audioSource.volume = Mathf.Lerp(_nodes[i].volume, _nodes[i+1].volume, _transitionPos);
        }
    }

    public void Play()
    {
        _audioSource = GetComponent<AudioSource>();
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