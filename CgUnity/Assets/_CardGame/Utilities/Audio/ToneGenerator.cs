using UnityEngine;

namespace MyUtilities
{
    public class ToneGenerator
    {
        public AudioClip Create(float frequency = 440f, float duration = 1.0f, int sampleRate = 44100)
        {
            int samples = Mathf.CeilToInt(duration * sampleRate);
            float[] data = new float[samples];

            float increment = 2f * Mathf.PI * frequency / sampleRate;
            float phase = 0f;
            for (int i = 0; i < samples; i++)
            {
                data[i] = Mathf.Sin(phase);
                phase += increment;
            }

            var clip = AudioClip.Create("Sine_" + frequency, samples, 1, sampleRate, false);
            clip.SetData(data, 0);
            return clip;
        }
    }
}