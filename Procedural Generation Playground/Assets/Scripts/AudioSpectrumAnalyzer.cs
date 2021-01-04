using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (AudioSource))]
public class AudioSpectrumAnalyzer : MonoBehaviour
{
    // Microphone Settings
    public AudioClip microphoneClip;
    public bool useMicrophone = false;

    public TMP_Dropdown deviceList;
   // public AudioMixerGroup microphoneMixer, masterMixer;

    public string selectedDevice;
    public List<string> options = new List<string>();

    [SerializeField] private int DEBUG_DEVICEINDEX;

    public static AudioSource AS;
    public AudioSource customAS, readAS;
    public static float[] samples = new float[512];
    public static float[] freqBands = new float[8];
    public static float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];
    [SerializeField] private bool useCustomAS;

    float[] freqBandHighest = new float[8];
    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];

    public float cameraShak = 1;
    public float Spectrum = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(customAS.clip);
        AS = GetComponent<AudioSource>();
        
        deviceList.ClearOptions();

        if(PlayerPrefs.GetInt("MicrophoneMode") == 0)
        {
            useMicrophone = false;
            readAS = AS;
        }
        else
        {
            useMicrophone = true;
            selectedDevice = PlayerPrefs.GetString("savedDevice");
            if (useMicrophone)
            {
                GetMicrophoneDevices();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

       // SetMicrophone(useMicrophone);

        GetSpectrumData(readAS);
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

    /// <summary>
    /// Stops the Audio Source, Loads the selected Device
    /// Injects the an empty clip into the audio source
    /// Records microphone input onto said empty clip
    /// </summary>
    private void UpdateMicrophone()
    {
        customAS.Stop(); // stop Audio Source
        selectedDevice = PlayerPrefs.GetString("deviceName"); // Loads a Saved Device Name
        customAS.clip = Microphone.Start(selectedDevice, true, 10, AudioSettings.outputSampleRate); // Injects empty clip and starts recording
        customAS.mute = false;
        AS.mute = true; 
        readAS = customAS;
        customAS.Play(); // Plays the Audio Source, you must play after starting the record.
    }

    /// <summary>
    /// This Searches for enabled Recording Devices
    /// </summary>
    public void GetMicrophoneDevices()
    {
        foreach (string device in Microphone.devices)
        {
            if(selectedDevice == null)
            {
                selectedDevice = device;
            }
            options.Add(device);
            selectedDevice = options[PlayerPrefs.GetInt("savedInt")];
            Debug.Log(selectedDevice);
        }
        deviceList.AddOptions(options);
        deviceList.value = PlayerPrefs.GetInt("DeviceID");
        deviceList.onValueChanged.AddListener(delegate { deviceListDropDownHandler(deviceList); });
        
        UpdateMicrophone();
    }


    /// <summary>
    /// Listens for a change from the device drop down on the UI
    /// </summary>
    /// <param name="dropdown"></param>
    public void deviceListDropDownHandler(TMP_Dropdown dropdown)
    {
        selectedDevice = options[deviceList.value];
        Debug.Log(selectedDevice);
        PlayerPrefs.SetInt("DeviceID", deviceList.value);
        PlayerPrefs.SetString("deviceName",selectedDevice);
        UpdateMicrophone();
    }

    /// <summary>
    /// Reloads the Scene to reset the Microphone recording and Audio Sorces
    /// </summary>
    public void ApplyMicrophoneSettings()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Switches to Microphone Mode
    /// </summary>
    public void UseMicrophone()
    {
        useMicrophone = !useMicrophone;
        if (useMicrophone)
        {
            PlayerPrefs.SetInt("MicrophoneMode", 1);
        }
        else
        {
            PlayerPrefs.SetInt("MicrophoneMode", 0);
        }
    }

    private void PlayListMode()
    {
        customAS.Stop();
        customAS.mute = true;
        AS.mute = false;
        readAS = AS;
        AS.Play();
    }
}