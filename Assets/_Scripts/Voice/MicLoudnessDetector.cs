using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicLoudnessDetector : MonoBehaviour
{
    private string micName;
    [SerializeField] private AudioSource micSource;
    private const int sampleWindow = 64;
    // Start is called before the first frame update
    void Start()
    {
        micName = Microphone.devices[0];
        Debug.Log("Mic is " + micName);
    }


    private void OnEnable()
    {
        micSource.clip = Microphone.Start(micName, true, 20, AudioSettings.outputSampleRate);
    }

    private void OnDisable()
    {
        Microphone.End(micName);
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Loudness "+ GetLoudness(Microphone.GetPosition(micName), micSource.clip));
    }

    float GetLoudness(int clipPos, AudioClip clip)
    {
        int startPos = clipPos - sampleWindow;
        if (startPos < 0 )
        {
            return 0;
        }

        float[] waveData = new float[sampleWindow];
        clip.GetData(waveData, startPos);

        float loudness = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            loudness += Mathf.Abs(waveData[i]);
        }

        return loudness / sampleWindow;
    }

    public void DisplayMicrophones()
    {
        for(int i = 0; i < Microphone.devices.Length; i++)
        {
            Debug.Log(Microphone.devices[i]);
            // Soon Add UI to choose the Mic then update name accordingly
        }
    }
}
