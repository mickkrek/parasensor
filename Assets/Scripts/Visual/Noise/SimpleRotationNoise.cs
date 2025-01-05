using System.Runtime.CompilerServices;
using UnityEngine;

namespace MovingOut2.Levels.Effects
{
    [AddComponentMenu("MO2/Levels/View Effects/Simple Rotation Noise")]
    public class SimpleRotationNoise : SimpleNoise
    {
        [SerializeField] bool resetRotationOnDisable = false;

        #region Noise Implementations

        protected static Vector3 PerlinNoise(float seed, float time, Vector3 frequency, Vector3 amplitude)
        {
            return new Vector3(
                (Mathf.PerlinNoise(seed + 0f, time * frequency.x) * 2f - 1f) * amplitude.x,
                (Mathf.PerlinNoise(seed + 1f, time * frequency.y) * 2f - 1f) * amplitude.y,
                (Mathf.PerlinNoise(seed + 2f, time * frequency.z) * 2f - 1f) * amplitude.z
            );
        }

        protected static Vector3 SineNoise(float seed, float time, Vector3 frequency, Vector3 amplitude)
        {
            return new Vector3(
                Mathf.Sin(seed + time * frequency.x) * amplitude.x,
                Mathf.Sin(seed + time * frequency.y) * amplitude.y,
                Mathf.Sin(seed + time * frequency.z) * amplitude.z
            );
        }

        #endregion

        private Quaternion m_InitialRotation;

        protected override void Start()
        {
            m_InitialRotation = transform.localRotation;
            base.Start();
        }

        protected override NoiseFunc GetNoiseFunction(NoiseFunction functionType)
        {
            switch (functionType)
            {
                case NoiseFunction.Perlin:
                default:
                    return PerlinNoise;

                case NoiseFunction.Sine:
                    return SineNoise;
            }
        }

        private void OnDisable()
        {
            if (resetRotationOnDisable)
                transform.localRotation = m_InitialRotation;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Apply(Vector3 noise) => transform.localRotation = m_InitialRotation * Quaternion.Euler(noise);
    }
}
