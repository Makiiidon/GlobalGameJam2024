using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GhostUIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI priestTimerText;
    private float ticks = 0.0f;
    private float maxTime = 60.0f;
    [SerializeField] private bool isGhostDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ticks < maxTime)
        {
            ticks += Time.deltaTime;
            priestTimerText.text = ticks.ToString();
        }
        else
        {
            isGhostDead = true;
            Debug.Log("Priest has arrived");
        }     
    }

    public bool IsGhostDead()
    {
        return isGhostDead;
    }
}
