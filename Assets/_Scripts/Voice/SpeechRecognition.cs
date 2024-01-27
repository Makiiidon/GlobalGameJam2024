using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;
using UnityEngine.UI;

public class SpeechRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    [SerializeField] private float minThreshold = 5.0f;
    [SerializeField] private float maxThreshold = 10.0f;
    [SerializeField] private float prevVolume = 0.0f;
    [SerializeField] private Slider meter;
    
    private MicLoudnessDetector mic;
    float outOfRangeTimer = 0.0f;
    [SerializeField] private float outOfRangeTime = 3.0f;
    [SerializeField] private int happinessMeter = 0;

    float timeStep = 0;
    [SerializeField] float timeInterval = 0.3f;

    [SerializeField] SpriteRenderer renderer;
    [SerializeField] Sprite neutralFace;
    [SerializeField] Sprite sadFace;
    [SerializeField] Sprite cryFace;
    [SerializeField] Sprite happyFace;

    [SerializeField] private string yes = "Yes";
    [SerializeField] private string no = "No";
    [SerializeField] private List<string> positive = new List<string>();
    [SerializeField] private List<string> negative = new List<string>();


    // Start is called before the first frame update
    void Start()
    {
        actions.Add(yes, YesFunction);        
        actions.Add(no, NoFunction);
        foreach (string str in negative)
        {
            actions.Add(str, NegativeFeedback);
        }
        foreach (string str in positive)
        {
            actions.Add(str, PositiveFeedback);
        }

        //Debug.Log("Dont worry this code works");
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizeSpeech;
        keywordRecognizer.Start();

        mic = GetComponent<MicLoudnessDetector>();
    }

    private void OnEnable()
    {
        if (keywordRecognizer == null) return;
        if (keywordRecognizer.IsRunning) return;
        if(!mic) mic = GetComponent<MicLoudnessDetector>();

        keywordRecognizer.Start();

    }
    private void OnDisable()
    {
        keywordRecognizer.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mic) mic = GetComponent<MicLoudnessDetector>();

        if (happinessMeter < -3)
        {
            happinessMeter = -3;
        }
        if (happinessMeter > 3)
        {
            happinessMeter = 3;
        }

        if (happinessMeter >= 2)
        {
            renderer.sprite = happyFace;
        } 
        else if (happinessMeter <= -2)
        {
            renderer.sprite = cryFace;
        }
        else if (happinessMeter == -1)
        {
            renderer.sprite = sadFace;
        }
        else
        {
            renderer.sprite = neutralFace;
        }

        if (timeInterval + timeStep < Time.time)
        {
            timeStep = Time.time + timeInterval;
        }
        else
        {
            return;
        }
        
        float volume = mic.GetSound();

        if (meter) meter.value = volume;
        if (volume < minThreshold || volume > maxThreshold)
        {
            // Volume is out of range
            outOfRangeTimer += Time.fixedDeltaTime;

            if (outOfRangeTimer >= outOfRangeTime)
            {
                // The volume has been out of range for yourDesiredDuration seconds
                Debug.Log("Volume out of range for too long");
                // Additional actions or logic can be added here

                // increase sad counter
                happinessMeter--;

                
            }
        }
        else
        {
            // Volume is within range, reset the timer
            outOfRangeTimer = 0.0f;

            // Additional logic when volume is within range can be added here
        }

        
    }


    void YesFunction()
    {
        Debug.Log("Yes");
    }

    void NoFunction()
    {
        Debug.Log("No");
    }

    void NegativeFeedback()
    {
        Debug.Log("Bad");
        happinessMeter--;
    }

    void PositiveFeedback()
    {
        Debug.Log("Good");
        happinessMeter++;
    }

    void RecognizeSpeech(PhraseRecognizedEventArgs speech)
    {
        //Debug.Log(speech);
        //if (!actions.ContainsKey(speech.text)) return;  
        actions[speech.text].Invoke();
        //actions.Remove(speech.text);
    }
}
