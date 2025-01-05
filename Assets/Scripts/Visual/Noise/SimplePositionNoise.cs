using System.Runtime.CompilerServices;
using UnityEngine;

namespace MovingOut2.Levels.Effects
{
    [AddComponentMenu("MO2/Levels/View Effects/Simple Position Noise")]
    public class SimplePositionNoise : SimpleNoise
    {
        #region Noise Implementations

        protected static Vector3 PerlinNoise(float seed, float time, Vector3 frequency, Vector3 amplitude)
        {
            return new Vector3(
                Mathf.PerlinNoise(seed + 0f, time * frequency.x) * amplitude.x,
                Mathf.PerlinNoise(seed + 1f, time * frequency.y) * amplitude.y,
                Mathf.PerlinNoise(seed + 2f, time * frequency.z) * amplitude.z
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

        private Vector3 m_InitialPosition;

        protected override void Start()
        {
            m_InitialPosition = transform.localPosition;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected override void Apply(Vector3 noise) => transform.localPosition = m_InitialPosition + noise;
    }
}
