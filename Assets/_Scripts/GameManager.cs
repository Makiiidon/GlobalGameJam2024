using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> games = new List<GameObject>();
    [SerializeField] GameObject mainCamera;

    public static GameManager Instance;
    int activeLevel = 0;

    bool win1 = false;
    bool win2 = false;
    bool win3 = false;
    bool win4 = false;
    bool win5 = false;
    bool win6 = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!Instance)
        {
            Instance = this;
        }
        activeLevel = 1;
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
        games[activeLevel - 1].SetActive(true);

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
                    // Add cutscene
                    break;
                }
            default:
                {
                    break;

                }
        }
        activeLevel++;
    }
}
