using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] float yOffset = 0.3f;

    [SerializeField] SpriteRenderer renderer;
    [SerializeField] Sprite neutralFace;
    [SerializeField] Sprite sadFace;
    [SerializeField] Sprite cryFace;
    [SerializeField] Sprite happyFace;
    [SerializeField] Sprite elatedFace;

    [SerializeField] ParticleSystem particleUp;
    [SerializeField] ParticleSystem particleDown;
    [SerializeField] ParticleSystem particleSparkle;
    [SerializeField] ParticleSystem particleQuestion;

    [SerializeField] private string yes = "Yes";
    [SerializeField] private string no = "No";
    [SerializeField] private List<string> positive = new List<string>();
    [SerializeField] private List<string> negative = new List<string>();

    [SerializeField] private AudioClip tyMom;
    [SerializeField] private AudioClip yesMom;
    [SerializeField] private AudioClip delightMom;
    [SerializeField] private AudioClip whyMom;
    [SerializeField] private AudioClip noMom;
    [SerializeField] private AudioClip meanMom;

    [SerializeField] TMP_Text tutorialText;
    bool startTutAnim = false;

    int gameOverCtr = 0;
    // Start is called before the first frame update
    void Start()
    {
        actions.Add(yes, YesFunction);        
        actions.Add(no, NoFunction);
        foreach (string str in negative)
        {
            if(!actions.ContainsKey(str))
                actions.Add(str, NegativeFeedback);
        }
        foreach (string str in positive)
        {
            if (!actions.ContainsKey(str))
                actions.Add(str, PositiveFeedback);
        }

        //Debug.Log("Dont worry this code works");
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizeSpeech;
        keywordRecognizer.Start();

        mic = GetComponent<MicLoudnessDetector>();
        StartCoroutine(AnimateTutorial());

    }

    private void OnEnable()
    {
        if (keywordRecognizer == null) return;
        if (keywordRecognizer.IsRunning) return;
        if(!mic) mic = GetComponent<MicLoudnessDetector>();

        keywordRecognizer.Start();
        StartCoroutine(AnimateTutorial()); 
    }
    private void OnDisable()
    {
        keywordRecognizer.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!mic) mic = GetComponent<MicLoudnessDetector>();

        if (startTutAnim)
        {
            float alpha = Mathf.Abs( Mathf.Sin(Time.time * 5) ) * 0.5f + 0.5f; // Scale to 0 to 1 range
            Color output = new Color(tutorialText.color.r, tutorialText.color.g, tutorialText.color.b, alpha);
            tutorialText.color = output;
        }
        else
        {
            if(tutorialText.color.a != 0)
            {
                float alpha = Mathf.Lerp(tutorialText.color.a, 0, Time.deltaTime * 10);
                Color output = new Color(tutorialText.color.r, tutorialText.color.g, tutorialText.color.b, alpha);
                tutorialText.color = output;
            }
        }

        if (happinessMeter < -4)
            happinessMeter = -4;

        if (happinessMeter > 3)
        {
            happinessMeter = 3;
            GameManager.Instance.SetWinState(5, true); 
            Debug.Log("Win");
        }


        if (happinessMeter >= 3)
            renderer.sprite = elatedFace;
        
        else if (happinessMeter >= 2)      
            renderer.sprite = happyFace;
        
        else if (happinessMeter <= -2)       
            renderer.sprite = cryFace;
        
        else if (happinessMeter == -1)    
            renderer.sprite = sadFace;
        
        else
            renderer.sprite = neutralFace;

        renderer.gameObject.transform.position = new Vector2(0, ((float)Math.Sin(Time.time)/4) + yOffset);

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
                //Debug.Log("Volume out of range for too long");
                // Additional actions or logic can be added here
                particleDown.Play();

                // increase sad counter
                happinessMeter--;
                outOfRangeTimer = 0.0f;
                if(happinessMeter < -3)
                    gameOverCtr++;
            }
        }
        else
        {
            // Volume is within range, reset the timer
            outOfRangeTimer = 0.0f;

            // Additional logic when volume is within range can be added here
        }

        if (gameOverCtr >= 3)
        {
            // Add Game Over
            GameManager.Instance.SetWinState(5, false);
            Debug.Log("You Lose");
        }
    }


    void YesFunction()
    {
        //Debug.Log("Yes");
    }

    void NoFunction()
    {
        //Debug.Log("No");
    }

    void NegativeFeedback()
    {
        int rand = UnityEngine.Random.Range(1, 4);
        Debug.Log(rand);
        switch (rand)
        {
            case 1:
                AudioManager.Instance.PlaySFX(meanMom);
                break;
            case 2:
                AudioManager.Instance.PlaySFX(noMom);
                break;
            case 3:
                AudioManager.Instance.PlaySFX(whyMom);
                break;
            case 4:
                AudioManager.Instance.PlaySFX(whyMom);
                break;
        }
        //Debug.Log("Bad");
        happinessMeter--;
        if (happinessMeter < -3)
            gameOverCtr++;
        particleDown.Play();
    }

    void PositiveFeedback()
    {
        int rand = UnityEngine.Random.Range(1, 4);
        Debug.Log(rand);
        switch (rand)
        {
            case 1:
                AudioManager.Instance.PlaySFX(tyMom);
                break;
            case 2:
                AudioManager.Instance.PlaySFX(delightMom);
                break;
            case 3:
                AudioManager.Instance.PlaySFX(yesMom);
                break;
            case 4:
                AudioManager.Instance.PlaySFX(yesMom);
                break;
        }
        //Debug.Log(word);
        happinessMeter++;
        if(happinessMeter >= 3)
            particleSparkle.Play();
        else
            particleUp.Play();
    }

    void RecognizeSpeech(PhraseRecognizedEventArgs speech)
    {
       
        //Debug.Log(speech.text);
        if (!actions.ContainsKey(speech.text))
        {
            particleQuestion.Play();
            return;
        }
        actions[speech.text].Invoke();
        actions.Remove(speech.text);
    }

    IEnumerator AnimateTutorial()
    {
        startTutAnim = true;
        yield return new WaitForSeconds(5.0f);
        startTutAnim = false;

    }
}
