using System.Runtime.CompilerServices;
using UnityEngine;

namespace MovingOut2.Levels.Effects
{
    public abstract class SimpleNoise : MonoBehaviour
    {
        protected delegate Vector3 NoiseFunc(float seed, float time, Vector3 frequency, Vector3 amplitude);

        public enum NoiseFunction
        {
            Perlin,
            Sine,
        }

        private float m_Seed;
        private NoiseFunc m_NoiseFunc;

#if UNITY_EDITOR
        // Used in editor for runtime modification via inspector
        private float m_VarFrequency;
        private float m_VarAmplitude;
        private NoiseFunction m_ActiveNoiseFunction;
#endif

        [SerializeField] private NoiseFunction m_NoiseFunction = NoiseFunction.Perlin;
        [Space]
        [SerializeField] private Vector3 m_Frequency = Vector3.one;
        [SerializeField] private float m_FrequencyVariation = default;
        [Space]
        [SerializeField] private Vector3 m_Amplitude = Vector3.one;
        [SerializeField] private float m_AmplitudeVariation = default;
        [Space]
        [SerializeField] private float m_Scale = 1.0f;

        #region Properties

        public float Scale
        {
            get => m_Scale;
            set => m_Scale = value;
        }

        #endregion

        #region MonoBehaviour

        protected virtual void Start()
        {
            m_Seed = UnityEngine.Random.Range(-100f, 0f);
            m_NoiseFunc = GetNoiseFunction(m_NoiseFunction);

            var varFrequency = UnityEngine.Random.Range(-m_FrequencyVariation, m_FrequencyVariation);
            var varAmplitude = UnityEngine.Random.Range(-m_AmplitudeVariation, m_AmplitudeVariation);

#if UNITY_EDITOR
            // In editor, store the randomised variation factors so they can be computed each frame
            // This is to support modifying the values in the inspector on the fly
            m_VarFrequency = varFrequency;
            m_VarAmplitude = varAmplitude;
#else
            // Outside of the editor we should just modify the values directly, as we can't modify
            // the noise parameters on the fly and can save the extra ops
            m_Frequency = ComputeVariation(m_Frequency, varFrequency);
            m_Amplitude = ComputeVariation(m_Amplitude, varAmplitude);
#endif
        }

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (Application.IsPlaying(this) && m_ActiveNoiseFunction != m_NoiseFunction)
            {
                m_NoiseFunc = GetNoiseFunction(m_NoiseFunction);
                m_ActiveNoiseFunction = m_NoiseFunction;
            }
        }
#endif

        private void Update()
        {
            Apply(ComputeNoise() * m_Scale);
        } 

        #endregion

        private Vector3 ComputeVariation(Vector3 input, float variation)
        {
            return new Vector3(
                input.x + input.x * variation,
                input.y + input.y * variation,
                input.z + input.z * variation
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Vector3 ComputeNoise()
        {
#if UNITY_EDITOR
            return m_NoiseFunc(m_Seed, Time.time,
                ComputeVariation(m_Frequency, m_VarFrequency),
                ComputeVariation(m_Amplitude, m_VarAmplitude));
#else
            return m_NoiseFunc(m_Seed, Time.time, m_Frequency, m_Amplitude);
#endif
        }

        #region Abstract

        protected abstract NoiseFunc GetNoiseFunction(NoiseFunction functionType);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract void Apply(Vector3 noise);

        #endregion
    }
}
