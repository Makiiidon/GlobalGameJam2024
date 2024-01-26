using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System.Linq;
using System;

public class SpeechRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    [SerializeField] private string yes = "Yes";
    [SerializeField] private string no = "No";
    // Start is called before the first frame update
    void Start()
    {
        actions.Add(yes, YesFunction);        
        actions.Add(no, NoFunction);

        Debug.Log("Dont worry this code works");
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizeSpeech;
        keywordRecognizer.Start();

    }

    private void OnEnable()
    {
        if (keywordRecognizer == null) return;
        if (keywordRecognizer.IsRunning) return;
        keywordRecognizer.Start();

    }
    private void OnDisable()
    {
        keywordRecognizer.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void YesFunction()
    {
        Debug.Log("Yes");
    }

    void NoFunction()
    {
        Debug.Log("No");
    }

    void RecognizeSpeech(PhraseRecognizedEventArgs speech)
    {
        //Debug.Log(speech);
        actions[speech.text].Invoke();
    }
}
