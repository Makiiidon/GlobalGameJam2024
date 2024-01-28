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
    [SerializeField] private float attackTicks = 0.0f; 
    [SerializeField] private float ATTACK_INTERVAL = 2.0f;
    [SerializeField] private float minAttackInterval = 2.0f;
    [SerializeField] private float maxAttackInterval = 3.0f;
    [SerializeField] private float playerPunchDelay;

    // Animator 
    [SerializeField] private Animator animator;
    [SerializeField] private GhostUIManager ghostUIManager;
    [SerializeField] private PlayerUIManager playerUIManager;


    // Start is called before the first frame update
    void Start()
    {
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerUIManager.GetIsInstructionsFinished())
        {
            if (ghostUIManager.IsGhostDead())
            {
                animator.enabled = true;
                animator.SetTrigger("didGhostDie");
            }

            if (!playerManager.CheckIfDead() && !ghostUIManager.IsGhostDead())
            {
                if (playerManager.GetIsPlayerPunching())
                {
                    attackTicks = 0.0f;
                    ATTACK_INTERVAL = maxAttackInterval;
                }
                else if (!playerManager.GetIsPlayerPunching())
                {
                    attackTicks += Time.deltaTime;
                    if (attackTicks >= ATTACK_INTERVAL && !playerManager.GetIsPlayerPunching())
                    {
                        attackTicks = 0.0f;

                        if (CheckIfPlayerPunching())  // Exit if player punching
                            return;

                        int chosenSide = Random.Range(1, 3); // If 1, Left, If 2, Right

                        if (CheckIfPlayerPunching())  // Exit if player punching
                            return;

                        if (chosenSide == 1 && !playerManager.GetIsPlayerPunching())
                        {
                            StartCoroutine(LeftPunch());
                        }
                        else if (chosenSide == 2 && !playerManager.GetIsPlayerPunching())
                        {
                            StartCoroutine(RightPunch());
                        }

                        ATTACK_INTERVAL = Random.Range(minAttackInterval, maxAttackInterval);
                    }
                }
            }
        }
    }

    IEnumerator LeftPunch()
    {
        // Show indicator first
        leftIndicator.SetActive(true);

        if (CheckIfPlayerPunching())  // Exit if player punching
            yield break;
        else
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

        if (CheckIfPlayerPunching())  // Exit if player punching
            yield break;
        else
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

    bool CheckIfPlayerPunching()
    {
        if (playerManager.GetIsPlayerPunching())
            return true;
        else
            return false;
    }

    public void SetBaseSprite()
    {
        spriteRenderer.flipX = false;
        spriteRenderer.sprite = baseSprite;
    }

    public void SetHitSprite()
    {
        spriteRenderer.sprite = hitSprite;
    }

    public void SetFlippedHitSprite()
    {
        spriteRenderer.flipX = true;
        spriteRenderer.sprite = hitSprite;
    }

    
}
