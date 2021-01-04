using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (AudioSource))]
public class AudioAnalyzer : MonoBehaviour
{
    public AudioSource AS;
    public float[] samples = new float[512];
    public float[] freqBands = new float[8];
    public float[] bandBuffer = new float[8];
    public float[] bufferDecrease = new float[8];

    public float[] freqBandHighest = new float[8];
    public float[] audioBand = new float[8];
    public float[] audioBandBuffer = new float[8];


    void Update()
    {
        GetSpectrumData(AS);
        GenerateFrequencyBands();
        BandBuffer();
        CreateAudioBands();
    }


    private void GetSpectrumData(AudioSource analyzedSource)
    {
        analyzedSource.GetSpectrumData(samples,0,FFTWindow.Blackman);
    }

    private void GenerateFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if( i == 8)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }
                average /= sampleCount;
            freqBands[i] = average * 10;
        }
    }

    private void BandBuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if(freqBands[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBands[i];
                bufferDecrease[i] = 0.05f;
            }

            if (freqBands[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
    }

    private void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if(freqBands[i] > freqBandHighest[i])
            {
                freqBandHighest[i] = freqBands[i];
            }

            audioBand[i] = (freqBands[i] / freqBandHighest[i]);
            audioBandBuffer[i] = (freqBands[i] / freqBandHighest[i]);



        }
    }

    
}