using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> games = new List<GameObject>();
    [SerializeField] GameObject mainCamera;

    int gameLevel = 0;

    bool win1 = false;
    bool win2 = false;
    bool win3 = false;
    bool win4 = false;
    bool win5 = false;
    bool win6 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
