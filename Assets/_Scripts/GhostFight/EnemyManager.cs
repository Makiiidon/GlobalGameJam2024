using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite hitSprite;
    [SerializeField] private Sprite leftPunchSprite;
    [SerializeField] private Sprite rightPunchSprite;
    [SerializeField] private GameObject leftIndicator;
    [SerializeField] private GameObject rightIndicator;

    // Punch
    private float attackTicks = 0.0f;
    [SerializeField] private float ATTACK_INTERVAL = 2.0f;
    [SerializeField] private float minAttackInterval = 2.0f;
    [SerializeField] private float maxAttackInterval = 3.0f;
    [SerializeField] private float playerPunchDelay;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        attackTicks += Time.deltaTime;
        if(attackTicks >= ATTACK_INTERVAL)
        {
            attackTicks = 0.0f;

            int chosenSide = Random.Range(1, 3); // If 1, Left, If 2, Right
            
            if(chosenSide == 1)
            {
                StartCoroutine(LeftPunch());
            }
            else if(chosenSide == 2) 
            {
                StartCoroutine(RightPunch());
            }

            ATTACK_INTERVAL = Random.Range(minAttackInterval, maxAttackInterval);   
        }
    }

    IEnumerator LeftPunch()
    {
        // Show indicator first
        leftIndicator.SetActive(true);
        yield return new WaitForSeconds(playerPunchDelay);

        spriteRenderer.sprite = leftPunchSprite;

        // Subtract health if player is dodging
        if (playerManager.GetIsDodging() && playerManager.GetIsDodgingLeft())
        {
            playerManager.SubtractHealth();
        }
        else if (!playerManager.GetIsDodging())
        {
            playerManager.SubtractHealth();
        }


        yield return new WaitForSeconds(playerPunchDelay);
        spriteRenderer.sprite = baseSprite;

        leftIndicator.SetActive(false);
    }

    IEnumerator RightPunch() 
    {
        // Show indicator first
        rightIndicator.SetActive(true);

        yield return new WaitForSeconds(playerPunchDelay);

        spriteRenderer.sprite = rightPunchSprite;

        // Subtract health if player is dodging
        if (playerManager.GetIsDodging() && !playerManager.GetIsDodgingLeft())
        {
            playerManager.SubtractHealth();
        }
        else if (!playerManager.GetIsDodging())
        {
            playerManager.SubtractHealth();
        }

        yield return new WaitForSeconds(playerPunchDelay);
        spriteRenderer.sprite = baseSprite;

        rightIndicator.SetActive(false);
    }

}
