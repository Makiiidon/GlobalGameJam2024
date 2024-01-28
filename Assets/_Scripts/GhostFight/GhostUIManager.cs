using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GhostUIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI priestTimerText;
    [SerializeField] private float ticks = 0.0f;
    private float maxTime = 60.0f;
    [SerializeField] private bool isGhostDead = false;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private PlayerUIManager playerUIManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerUIManager.GetIsInstructionsFinished())
        {
            if (ticks < maxTime)
            {
                ticks += Time.deltaTime;
                priestTimerText.text = ticks.ToString();
            }
            else
            {
                playerManager.SetCanBeDamaged(false);
                isGhostDead = true;
                Debug.Log("Priest has arrived");
                GameManager.Instance.SetWinState(6, true);
            }
        }
    }

    public bool IsGhostDead()
    {
        return isGhostDead;
    }
}
