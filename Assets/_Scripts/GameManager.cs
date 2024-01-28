using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> games = new List<GameObject>();
    [SerializeField] List<GameObject> frames = new List<GameObject>();
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject btn;
    [SerializeField] List<string> texts = new List<string>();
    [SerializeField] TMP_Text word;

    public static GameManager Instance;
    [SerializeField] int activeLevel = 0;

    bool win1 = false;
    bool win2 = false;
    bool win3 = false;
    bool win4 = false;
    bool win5 = false;
    bool win6 = false;

    bool fadeText = false;
    int wordCtr = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (!Instance)
        {
            Instance = this;
        }
        activeLevel = 1;
        //StartTransition(1);
        SetActiveLevel(activeLevel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetActiveLevel(int level)
    {
        activeLevel = level;
        foreach (GameObject game in games)
        {
            game.SetActive(false);
        }
        StartTransition();
        games[activeLevel - 1].SetActive(true);

    }

    public void IncrementLevel()
    {
        activeLevel++;
        
        SetActiveLevel(activeLevel);
    }

    public void SetWinState(int level, bool isWin)
    {
        switch (level)
        {
            case 1:
                {
                    win1 = isWin;
                    // Go to next
                    break;
                }
            case 2:
                {
                    win2 = isWin;
                    // Go to next
                    break;
                }
            case 3:
                {
                    win3 = isWin;
                    // Go to next
                    break;
                }
            case 4:
                {
                    win4 = isWin;
                    // Go to next
                    break;
                }
            case 5:
                {
                    win5 = isWin;
                    // Go to next
                    break;
                }
            case 6:
                {
                    win6 = isWin;
                    games[games.Count - 1].SetActive(true) ;
                    // Add cutscene
                    break;
                }
            default:
                {
                    break;

                }
        }
        games[activeLevel-1].SetActive(false);
        activeLevel++;
        StartTransition();
    }

    void StartTransition()
    {
        if (activeLevel == 3 ||
            activeLevel == 5 ||
            activeLevel == 7 ||
            activeLevel == 9 ||
            activeLevel == 11 ||
            activeLevel == 13 
            )
        {
            if (activeLevel == 3 || activeLevel == 9)
                mainCamera.SetActive(false);

            if (activeLevel == 9)
            {
                canvas.SetActive(false);
            }

            foreach (GameObject frame in frames)
            {
                frame.SetActive(false);
            }

            btn.SetActive(false);
            word.gameObject.SetActive(false);
            return;

        }
        
        word.text = texts[wordCtr];

        //frames[num].SetActive(true);
        btn.SetActive(true);
        word.gameObject.SetActive(true);
        mainCamera.SetActive(true);
        canvas.SetActive(true);

        wordCtr++;
    }

}
